using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyApiProject.Application.DTOs;
using MyApiProject.Application.Mappings;
using MyApiProject.Domain.Entities;
using MyApiProject.Infrastructure;
using MyApiProject.Infrastructure.Middleware;
using MyApiProject.Infrastructure.Persistence;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddHttpClient();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddInfrastructure();

// ---------- Swagger (single registration, standard Bearer scheme) ----------
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Address API", Version = "v1" });

    c.MapType<DmsIdentifier>(() => new OpenApiSchema
    {
        Type = "object",
        Properties = new Dictionary<string, OpenApiSchema>
        {
            { "internalId", new OpenApiSchema { Type = "string" } },
            { "externalId", new OpenApiSchema { Type = "string" } }
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Paste only the JWT. Swagger adds the 'Bearer ' prefix."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

// ---------- Database ----------
var useInMemory = builder.Configuration.GetValue<bool>("UseInMemoryDatabase");

if (useInMemory)
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase("CRMTestDB"));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseNpgsql(
                builder.Configuration.GetConnectionString("PostgreSQL"),
                b => b.MigrationsAssembly("MyApiProject.Infrastructure"))
            .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));

        // A09/A02: SQL parameter values and console SQL logging only in Development
        if (builder.Environment.IsDevelopment())
        {
            options.EnableSensitiveDataLogging()
                   .LogTo(Console.WriteLine, LogLevel.Information);
        }
    });
}

// ---------- JWT settings (env vars are already part of builder.Configuration) ----------
var jwtSettings = new JwtSettings
{
    Issuer = builder.Configuration["JWT_ISSUER"],
    Audience = builder.Configuration["JWT_AUDIENCE"],
    Key = builder.Configuration["JWT_KEY"],
    ExpiryMinutes = int.TryParse(builder.Configuration["JWT_EXPIRY_MINUTES"], out var minutes) ? minutes : 60
};

// A02: fail fast with a clear message instead of starting with a weak or missing key
if (string.IsNullOrWhiteSpace(jwtSettings.Issuer) ||
    string.IsNullOrWhiteSpace(jwtSettings.Audience) ||
    string.IsNullOrWhiteSpace(jwtSettings.Key))
{
    throw new InvalidOperationException("JWT_ISSUER, JWT_AUDIENCE and JWT_KEY must be configured.");
}

var keyBytes = Encoding.UTF8.GetBytes(jwtSettings.Key);
if (keyBytes.Length < 32)
{
    throw new InvalidOperationException("JWT_KEY must be at least 32 bytes (256 bits) for HS256.");
}

builder.Services.Configure<JwtSettings>(options =>
{
    options.Issuer = jwtSettings.Issuer;
    options.Audience = jwtSettings.Audience;
    options.Key = jwtSettings.Key;
    options.ExpiryMinutes = jwtSettings.ExpiryMinutes;
});

// ---------- Authentication / Authorization ----------
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
    options.IncludeErrorDetails = builder.Environment.IsDevelopment();
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 },
        ClockSkew = TimeSpan.FromSeconds(30)
    };

    // Backward compatibility: accept the raw JWT from X-Authorization.
    // The standard Authorization: Bearer header still works.
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            if (string.IsNullOrEmpty(context.Token) &&
                context.Request.Headers.TryGetValue("X-Authorization", out var value))
            {
                var token = value.ToString();
                context.Token = token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
                    ? token["Bearer ".Length..].Trim()
                    : token.Trim();
            }
            return Task.CompletedTask;
        }
    };
});

// A01: secure by default. Endpoints without [Authorize] now require authentication.
// Mark only the login endpoint with [AllowAnonymous].
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

// ---------- Rate limiting (A07) ----------
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    // Strict limit for login: use [EnableRateLimiting("login")] on the login action
    options.AddPolicy("login", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            }));

    // General per-IP limit for everything else
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 100,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            }));
});

// ---------- CORS (A05): explicit origins only ----------
var allowedOrigins = builder.Configuration.GetSection("Cors:Origins").Get<string[]>() ?? Array.Empty<string>();
builder.Services.AddCors(o => o.AddPolicy("Frontend", p =>
    p.WithOrigins(allowedOrigins)
     .WithHeaders("Authorization", "Content-Type")
     .WithMethods("GET", "POST", "PUT", "DELETE")));

// Limit request body size (A04): 1 MB
builder.WebHost.ConfigureKestrel(k => k.Limits.MaxRequestBodySize = 1_048_576);

// Required for EF Core / Npgsql timestamp handling
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

// ---------- Migrations: automatic only in Development ----------
// In production, apply migrations from the CI/CD pipeline (dotnet ef migrations bundle / script).
if (!useInMemory && app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

// Security headers
app.Use(async (context, next) =>
{
    var headers = context.Response.Headers;
    headers["X-Content-Type-Options"] = "nosniff";
    headers["X-Frame-Options"] = "DENY";
    headers["Referrer-Policy"] = "no-referrer";
    headers["Cache-Control"] = "no-store";
    await next();
});

app.UseMiddleware<ExceptionMiddleware>(); // must return a generic message in production
app.UseHttpsRedirection();
app.UseCors("Frontend");
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
