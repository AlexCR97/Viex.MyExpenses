using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Viex.MyExpenses.Persistence.Repositories.TransactionCategoryDescriptors
{
    public interface ITransactionCategoryDescriptorRepository : IDescriptorRepository<TransactionCategoryDescriptor>
    {

    }

    public class TransactionCategoryDescriptorRepository : ITransactionCategoryDescriptorRepository
    {
        private readonly MyExpensesContext _context;

        public TransactionCategoryDescriptorRepository(MyExpensesContext context)
        {
            _context = context;
        }

        public async Task<IList<int>> CreateAll(IEnumerable<TransactionCategoryDescriptor> entities)
        {
            await _context.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities.Select(x => x.TransactionCategoryDescriptorId).ToList();
        }

        public void DropAll() =>
            _context.TransactionCategoryDescriptors.Clear();

        public async Task<IList<TransactionCategoryDescriptor>> Get() =>
            await _context.TransactionCategoryDescriptors.ToListAsync();

        public async Task<TransactionCategoryDescriptor> GetByDescription(string description) =>
            await _context.TransactionCategoryDescriptors.FirstOrDefaultAsync(x => x.Description == description);

        public async Task<TransactionCategoryDescriptor> GetFirst(Expression<Func<TransactionCategoryDescriptor, bool>> predicate) =>
            await _context.TransactionCategoryDescriptors.FirstOrDefaultAsync(predicate);
    }
}
