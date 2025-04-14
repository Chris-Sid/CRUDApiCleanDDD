using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyApiProject.Application.Interfaces;
using MyApiProject.Infrastructure.Helpers;
using MyApiProject.Infrastructure.Logging;
using MyApiProject.Infrastructure.Services.Address;
using MyApiProject.Infrastructure.Services.Session;
using NLog;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
           // LogManager.LoadConfiguration("nlog.config"); // Optional, but useful
            services.AddSingleton(typeof(IAppLogger<>), typeof(NLogLogger<>));
            services.AddHttpClient();
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"));
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<IAddressService, AddressService>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var baseUrl = "https://localhost:7093/swagger/index.html"; // Retrieve the value from appsettings.json or environment variables
                                                                           // var requestHeaders = new AddressRequestHeaders(); // If headers are default or empty
                return new AddressService(provider.GetRequiredService<IHttpClientFactory>(), baseUrl);
            });
            return services;
        }
    }
}
