using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate;

namespace PharmacyCalendar.Infrastructure.EntityConfiguration
{
    public class TechnicalOfficerWorkshiftConfiguration : IEntityTypeConfiguration<TechniacalOfficerWorkShift>
    {
        public void Configure(EntityTypeBuilder<TechniacalOfficerWorkShift> builder)
        {
            builder.ToTable("TechnicalOfficerWorkshift");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.WorkShift).IsRequired().HasConversion<int>();
            builder.Property(p => p.Weekdays).IsRequired().HasConversion<int>();

            builder.HasOne(e => e.TechnicalOfficer)
             .WithMany(t => t.TechniacalOfficerWorkShift)
             .HasForeignKey(e => e.TechnicalOfficerId)
             .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
