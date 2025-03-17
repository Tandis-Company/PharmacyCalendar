using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate;

namespace PharmacyCalendar.Infrastructure.EntityConfiguration
{
    public class TechnicalOfficerConfiguration: IEntityTypeConfiguration<TechnicalOfficer>
    {
        public void Configure(EntityTypeBuilder<TechnicalOfficer> builder)
        {
            builder.ToTable("TechnicalOfficer");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.FullName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.NationalCode).IsRequired().HasMaxLength(10);
        }
    }
}
