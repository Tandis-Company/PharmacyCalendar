using Utilities.Framework.Contracts;

namespace PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate.Contracts
{
    public interface ITechnicalOfficerRepository : IWriteRepository<TechnicalOfficer>, IReadRepository<TechnicalOfficer>
    {
        Task<TechnicalOfficerWorkshift> AddAsync(TechnicalOfficerWorkshift entity, CancellationToken cancellationToken = default, bool saveChanges = true);
        Task<IEnumerable<TechnicalOfficer>> GetAllAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
