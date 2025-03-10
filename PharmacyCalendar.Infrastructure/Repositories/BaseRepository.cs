using Microsoft.EntityFrameworkCore;
using Utilities.Framework.Contracts;

namespace PharmacyCalendar.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IWriteRepository<TEntity>, IReadRepository<TEntity>
       where TEntity : class
    {
        private readonly PharmacyCalendarDbContext dbContext;

        #region [- Ctor -]

        public BaseRepository(PharmacyCalendarDbContext dbContext)
        {
                this.dbContext=dbContext;
        }

        #endregion

        #region [- Add() -]

        public TEntity Add(TEntity entity)
        {
            return dbContext.Add(entity).Entity;
        }

        #endregion

        #region [- AddAsync() -]

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default, bool saveChanges = true)
        {
            await dbContext.AddAsync(entity, cancellationToken);
            if (saveChanges)
                await SaveChangeAsync(cancellationToken);
            return entity;
        }

        #endregion

        #region [- Delete() -]

        public TEntity Delete(TEntity entity)
        {
            return dbContext.Remove(entity).Entity;
        }

        #endregion

        #region [- DeleteAsync() -]

        public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var deleted = await Task.FromResult(dbContext.Remove(entity).Entity);
            await SaveChangeAsync(cancellationToken);
            return deleted;
        }

        #endregion

        #region [- GetAllAsync() -]

        public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return dbContext.Set<TEntity>().ToListAsync(cancellationToken);
        }

        #endregion

        #region [- GetByIdAsync() -]

        public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await dbContext.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
        }

        #endregion

        #region [- SaveChangeAsync() -]

        public Task SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            return dbContext.SaveChangesAsync(cancellationToken);
        }

        #endregion

    }
}
