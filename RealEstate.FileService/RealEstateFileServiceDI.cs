using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Application.Contracts;

namespace RealEstate.FileService
{
    public static class RealEstateFileServiceDI
    {
        public static IServiceCollection AddRealEstateFileService(this IServiceCollection services)
        {
            services.AddScoped<IFileService,FileService>();
            return services;
        }
    }
}
