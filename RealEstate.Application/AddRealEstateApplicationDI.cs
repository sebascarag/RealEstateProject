using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Application.Behaviours;
using System.Reflection;

namespace RealEstate.Application
{
    public static class AddRealEstateApplicationDI
    {
        public static IServiceCollection AddRealEstateApplication(this IServiceCollection services, IConfiguration configuration)
        {
            var baseAssembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(baseAssembly));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));

            // fluent
            services.AddValidatorsFromAssembly(baseAssembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            ValidatorOptions.Global.LanguageManager.Enabled = false;
            
            services.AddAutoMapper(baseAssembly);

            return services;
        }
    }
}