using RealEstate.Api.Services;
using RealEstate.Application.Contracts;

namespace RealEstate.Api
{
    public static class RealEstateApiDI
    {
        public static void AddRealEstateApi(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
        }
    }
}
