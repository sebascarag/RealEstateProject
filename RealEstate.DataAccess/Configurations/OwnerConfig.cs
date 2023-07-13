using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Domain.Entities;

namespace RealEstate.DataAccess.Configurations
{
    public class OwnerConfig : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            // relations
            builder.HasMany(b => b.Properties);

            // seeds
            var now = DateTime.UtcNow;
            builder.HasData(
                new Owner
                {
                    Id = 1,
                    Name = "Sheri D. Coffelt",
                    Address = "3071 Todds Lane San Antonio, TX 78205",
                    Photo = "984bb1a4-46d8-4fbd-b9b1-fa78d395be7b.png",
                    Birthday = new DateTime(1982, 1, 27),
                    Active = true,
                    CreatedBy = "System",
                    CreatedOn = now,
                    ModifiedBy = "System",
                    ModifiedOn = now,
                }
            );

            builder.HasData(
                new Owner
                {
                    Id = 2,
                    Name = "Donnie K. Russell",
                    Address = "1641 Despard Street Atlanta, GA 30309",
                    Photo = "ffc8ea6a-10c6-4997-916c-55b20910158c.png",
                    Birthday = new DateTime(1994, 5, 29),
                    Active = true,
                    CreatedBy = "System",
                    CreatedOn = now,
                    ModifiedBy = "System",
                    ModifiedOn = now,
                }
            );

            builder.HasData(
                new Owner
                {
                    Id = 3,
                    Name = "Jeff S. Nelson",
                    Address = "1271 Hillcrest Circle Golden Valley, MN 55427",
                    Photo = "d73beddb-805f-47ca-baac-d9a2dbfb92ec.png",
                    Birthday = new DateTime(1986, 12, 05),
                    Active = true,
                    CreatedBy = "System",
                    CreatedOn = now,
                    ModifiedBy = "System",
                    ModifiedOn = now,
                }
            );
        }
    }
}
