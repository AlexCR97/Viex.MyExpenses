using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Viex.MyExpenses.Persistence.Repositories.TransactionSubCategoryDescriptors
{
    public interface ITransactionSubCategoryDescriptorRepository : IDescriptorRepository<TransactionSubCategoryDescriptor>
    {

    }

    public class TransactionSubCategoryDescriptorRepository : ITransactionSubCategoryDescriptorRepository
    {
        private readonly MyExpensesContext _context;

        public TransactionSubCategoryDescriptorRepository(MyExpensesContext context)
        {
            _context = context;
        }

        public async Task<IList<int>> CreateAll(IEnumerable<TransactionSubCategoryDescriptor> entities)
        {
            await _context.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities.Select(x => x.TransactionSubCategoryDescriptorId).ToList();
        }

        public void DropAll() =>
            _context.TransactionSubCategoryDescriptors.Clear();

        public async Task<IList<TransactionSubCategoryDescriptor>> Get() =>
            await _context.TransactionSubCategoryDescriptors.ToListAsync();

        public async Task<TransactionSubCategoryDescriptor> GetByDescription(string description) =>
            await _context.TransactionSubCategoryDescriptors
            .FirstOrDefaultAsync(x => x.Description == description);

        public async Task<TransactionSubCategoryDescriptor> GetFirst(Expression<Func<TransactionSubCategoryDescriptor, bool>> predicate) =>
            await _context.TransactionSubCategoryDescriptors
            .FirstOrDefaultAsync(predicate);
    }
}
