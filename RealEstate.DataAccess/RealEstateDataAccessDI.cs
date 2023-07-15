using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Application.Contracts;
using RealEstate.DataAccess.Identity;
using RealEstate.DataAccess.Interceptors;
using RealEstate.DataAccess.Repository;

namespace RealEstate.DataAccess
{
    public static class RealEstateDataAccessDI
    {
        public static IServiceCollection AddRealEstateDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuditableEntitySaveChangesInterceptor>(); // set audits props before save
            services.AddDbContext<RealEstateDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<RealEstateDbContextInitializer>(); // intialize database

            // use same db contexto for identity
            services.AddIdentityCore<ApplicationUser>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<RealEstateDbContext>();

            services.AddScoped<IIdentityService, IdentityService>(); // provide user access actions
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
    }
}
