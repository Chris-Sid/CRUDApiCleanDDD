using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyApiProject.API.AuthMiddleware;
using MyApiProject.Application.DTOs;
using MyApiProject.Domain.Entities;
using MyApiProject.Infrastructure;

using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddHttpClient();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Address API",
        Version = "v1"
    });
    // Optional: Customize schema for DmsIdentifier
    c.MapType<DmsIdentifier>(() => new Microsoft.OpenApi.Models.OpenApiSchema
    {
        Type = "object",
        Properties = new Dictionary<string, Microsoft.OpenApi.Models.OpenApiSchema>
        {
            { "internalId", new Microsoft.OpenApi.Models.OpenApiSchema { Type = "string" } },
            { "externalId", new Microsoft.OpenApi.Models.OpenApiSchema { Type = "string" } }
        }
    });
    c.AddSecurityDefinition("X-Authorization", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        Name = "X-Authorization",
        In = ParameterLocation.Header,
        Description = "Enter your API token here"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "X-Authorization"
                }
            },
            new string[] {}
        }
    });
});
 
var config = builder.Configuration;

builder.Configuration.AddEnvironmentVariables(); // Enable environment variables

var jwtSettings = new JwtSettings
{
    Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ,
    Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
    Key = Environment.GetEnvironmentVariable("JWT_KEY") ?? "defaultKey",
    ExpiryMinutes = int.TryParse(Environment.GetEnvironmentVariable("JWT_EXPIRY_MINUTES"), out var minutes) ? minutes : 60
};

builder.Services.Configure<JwtSettings>(options =>
{
    options.Issuer = jwtSettings.Issuer;
    options.Audience = jwtSettings.Audience;
    options.Key = jwtSettings.Key;
    options.ExpiryMinutes = jwtSettings.ExpiryMinutes;
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

.AddJwtBearer(options =>
{
    options.IncludeErrorDetails = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
    };
});


var app = builder.Build();
 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Use(async (context, next) =>
{
    if (context.Request.Headers.TryGetValue("X-Authorization", out var token))
    {
        context.Request.Headers["Authorization"] = $"Bearer {token}";
    }
    await next();
});

app.UseMiddleware<DebugJwtMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

//app.UseMiddleware<AuthMiddleware>();

app.MapControllers();

app.Run();
