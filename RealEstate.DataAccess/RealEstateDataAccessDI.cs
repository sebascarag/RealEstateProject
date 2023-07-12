using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.DataAccess.Repository;
using RealEstate.Domain.Entities;

namespace RealEstate.DataAccess
{
    public static class RealEstateDataAccessDI
    {
        public static IServiceCollection AddRealEstateDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RealEstateDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
    }
}
