using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Viex.MyExpenses.Persistence.Repositories
{
    public interface IDescriptorRepository<T> where T : BaseDescriptorEntity
    {
        Task<IList<int>> CreateAll(IEnumerable<T> entities);
        void DropAll();
        Task<IList<T>> Get();
        Task<T> GetByDescription(string description);
        Task<T> GetFirst(Expression<Func<T, bool>> predicate);
    }
}
