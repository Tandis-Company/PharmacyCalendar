using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Framework.Contracts
{
    public interface IWriteRepository<TEntity> where TEntity : AggregateRoot
    {
        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default, bool saveChanges = true);

        TEntity Delete(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task SaveChangeAsync(CancellationToken cancellationToken = default);
    }
}
