using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Domain.Constants;

namespace RealEstate.DataAccess
{
    public static class InitialiserExtensions
    {
        public static async Task InitializeDatabaseAsync(this WebApplication app)
        {
            // Initialization database process when app start on dev
            using var scope = app.Services.CreateScope();

            var initialiser = scope.ServiceProvider.GetRequiredService<RealEstateDbContextInitializer>();

            await initialiser.InitializeAsync();

            await initialiser.SeedAsync();
        }
    }

    public class RealEstateDbContextInitializer
    {
        private readonly RealEstateDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RealEstateDbContextInitializer(RealEstateDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task InitializeAsync()
        {
            try
            {
                // create the database if it doesn't already exist, apply any pending migrations and apply seed data
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while initialising the database. Error: {ex.Message}");
                throw;
            }
        }

        public async Task SeedAsync() // aditional seed data
        {
            try
            {
                // Default roles
                var administratorRole = new IdentityRole(Roles.Administrator);

                if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
                    await _roleManager.CreateAsync(administratorRole);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while seeding the database. Error: {ex.Message}");
                throw;
            }
        }
    }
}
