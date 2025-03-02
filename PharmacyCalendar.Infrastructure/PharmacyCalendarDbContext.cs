using Microsoft.EntityFrameworkCore;
using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate;
using PharmacyCalendar.Infrastructure.Configuration;
using System.Reflection;
using Utilities.Framework.Contracts;

namespace PharmacyCalendar.Infrastructure
{
    public class PharmacyCalendarDbContext : DbContext
    {
        public PharmacyCalendarDbContext(DbContextOptions<PharmacyCalendarDbContext> options) : base(options)
        {
            
        }

        #region [- OnModelCreating(ModelBuilder modelBuilder) -]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.RegisterAllEntities<IDbSetEntity>(typeof(IDbSetEntity).Assembly);

            base.OnModelCreating(modelBuilder);
        }
        #endregion

        public virtual DbSet<TechnicalOfficer> TechnicalOfficers { get; set; }
    }
}
