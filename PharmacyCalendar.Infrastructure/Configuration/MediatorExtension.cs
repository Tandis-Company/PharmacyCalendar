using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Framework;

namespace PharmacyCalendar.Infrastructure.Configuration
{
     public static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, PharmacyCalendarDbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<AggregateRoot>()
                .Where(x => x.Entity.GetEvents != null && x.Entity.GetEvents().Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.GetEvents())
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}
