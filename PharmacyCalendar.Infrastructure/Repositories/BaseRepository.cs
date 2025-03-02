using Microsoft.EntityFrameworkCore;
using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Framework;
using Utilities.Framework.Contracts;

namespace PharmacyCalendar.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IWriteRepository<TEntity>, IReadRepository<TEntity>
       where TEntity : AggregateRoot
    {
        private readonly PharmacyCalendarDbContext dbContext;
        public BaseRepository(PharmacyCalendarDbContext dbContext)
        {
                this.dbContext=dbContext;
        }

        public TEntity Add(TEntity entity)
        {
            return dbContext.Add(entity).Entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default, bool saveChanges = true)
        {
            await dbContext.AddAsync(entity, cancellationToken);
            if (saveChanges)
                await SaveChangeAsync(cancellationToken);
            return entity;
        }

        public TEntity Delete(TEntity entity)
        {
            return dbContext.Remove(entity).Entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var deleted = await Task.FromResult(dbContext.Remove(entity).Entity);
            await SaveChangeAsync(cancellationToken);
            return deleted;
        }

        public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return dbContext.Set<TEntity>().ToListAsync(cancellationToken);
        }

        public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await dbContext.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
        }

        public Task SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            return dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
