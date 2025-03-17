using Utilities.Framework.Contracts;

namespace PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate.Contracts
{
    public interface ITechnicalOfficerRepository : IWriteRepository<TechnicalOfficer>, IReadRepository<TechnicalOfficer>
    {
        Task<TechniacalOfficerWorkShift> AddAsync(TechniacalOfficerWorkShift entity, CancellationToken cancellationToken = default, bool saveChanges = true);
    }
}
