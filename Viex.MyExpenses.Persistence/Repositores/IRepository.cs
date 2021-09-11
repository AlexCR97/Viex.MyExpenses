using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Viex.MyExpenses.Persistence.Repositores
{
    public interface IRepository<T>
    {
        Task<long> Create(T entity);
        Task Delete(long id);
        Task<T> GetById(long id);
        Task<T> GetFirst(Expression<Func<T, bool>> predicate);
        Task<IList<T>> GetWhere(Expression<Func<T, bool>> predicate);
        Task Update(T entity);
    }
}
