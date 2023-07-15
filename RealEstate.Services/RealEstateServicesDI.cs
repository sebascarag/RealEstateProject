using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Application.Contracts;
using RealEstate.Services.Authentication;
using RealEstate.Services.Authentication.Configs;
using RealEstate.Services.FileServices;

namespace RealEstate.Services
{
    public static class RealEstateServicesDI
    {
        public static IServiceCollection AddRealEstateServices(this IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();

            // Add JWT token Auth
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
            services.ConfigureOptions<JwtOptionsConfig>();
            services.ConfigureOptions<JwtBearerOptionsConfig>();

            return services;
        }
    }
}
