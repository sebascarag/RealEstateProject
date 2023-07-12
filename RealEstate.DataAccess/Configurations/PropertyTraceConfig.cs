using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Domain.Entities;

namespace RealEstate.DataAccess.Configurations
{
    public class PropertyTraceConfig : IEntityTypeConfiguration<PropertyTrace>
    {
        public void Configure(EntityTypeBuilder<PropertyTrace> builder)
        {
            // properties
            builder.Property(p => p.Value).HasPrecision(14, 2);
            builder.Property(p => p.Tax).HasPrecision(14, 2);

            // relations
            builder.HasOne(x => x.Property);
        }
    }
}
