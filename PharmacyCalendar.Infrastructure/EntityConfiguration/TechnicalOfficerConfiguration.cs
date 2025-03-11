using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
