using Microsoft.EntityFrameworkCore;
using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate;
using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate.Contracts;

namespace PharmacyCalendar.Infrastructure.Repositories
{
    public class TechnicalOfficerRepository : BaseRepository<TechnicalOfficer>, ITechnicalOfficerRepository
    {
        private readonly PharmacyCalendarDbContext _context;
        public TechnicalOfficerRepository(PharmacyCalendarDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<TechniacalOfficerWorkShift> AddAsync(TechniacalOfficerWorkShift entity, CancellationToken cancellationToken = default, bool saveChanges = true)
        {
            await _context.AddAsync(entity, cancellationToken);
            if (saveChanges)
                await SaveChangeAsync(cancellationToken);
            return entity;
        }

        public async Task<IEnumerable<TechnicalOfficer>> GetAllAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.TechnicalOfficers.Where(g => g.Id == id).ToListAsync(cancellationToken);
        }
    }
}
