using Microsoft.EntityFrameworkCore;
using RealEstate.DataAccess.Interceptors;
using RealEstate.Domain.Entities;
using System.Reflection;

namespace RealEstate.DataAccess
{
    public class RealEstateDbContext : DbContext
    {
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
        public DbSet<Owner> Owners { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<PropertyTrace> PropertyTraces { get; set; }
        public DbSet<Property> Properties { get; set; }

        public RealEstateDbContext() { }
        public RealEstateDbContext(DbContextOptions options, AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
        {
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                // string temporary connection for build database
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;database=RealEstate;Trusted_Connection=True;MultipleActiveResultSets=True;");

            if (_auditableEntitySaveChangesInterceptor != null)
                optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // use Entities Configurations
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}