using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.DataAccess;
using System.Reflection;

namespace RealEstate.Application
{
    public static class AddRealEstateApplicationDI
    {
        public static IServiceCollection AddAddRealEstateApplication(this IServiceCollection services, IConfiguration configuration)
        {
            var baseAssembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(baseAssembly));
            services.AddValidatorsFromAssembly(baseAssembly);
            services.AddAutoMapper(baseAssembly);
            services.AddRealEstateDataAccess(configuration);
            return services;
        }
    }
}