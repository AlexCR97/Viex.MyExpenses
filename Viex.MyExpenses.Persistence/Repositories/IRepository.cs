using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Viex.MyExpenses.Persistence.Repositories
{
    public interface IRepository<TEntity, TId>
    {
        Task<TId> Create(TEntity entity);
        Task Delete(TId id);
        Task<TEntity> GetById(TId id);
        Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> predicate);
        Task<IList<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate);
        Task Update(TEntity entity);
    }
}
