using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Viex.MyExpenses.Persistence.Repositores
{
    public interface IAssociationRepository<T> where T : BaseEntity
    {
        Task<IList<long>> CreateAll(IEnumerable<T> entities);
        Task DeleteWhere(Expression<Func<T, bool>> predicate);
        void DropAll();
        Task<IList<T>> Get();
        Task<T> GetFirst(Expression<Func<T, bool>> predicate);
        Task<IList<T>> GetWhere(Expression<Func<T, bool>> predicate);
    }
}
