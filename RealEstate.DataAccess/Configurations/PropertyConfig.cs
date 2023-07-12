using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Domain.Entities;

namespace RealEstate.DataAccess.Configurations
{
    public class PropertyConfig : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            // properties
            builder.Property(p => p.Price).HasPrecision(14, 2);

            // relations
            builder.HasOne(b => b.Owner);
            builder.HasMany(b => b.PropertyImages);
            builder.HasMany(b => b.PropertyTraces);
        }
    }
}
