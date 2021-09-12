using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Viex.MyExpenses.Persistence.Repositores.TransactionTypeDescriptors
{
    public interface ITransactionTypeDescriptorsRepository : IDescriptorRepository<TransactionTypeDescriptor>
    {

    }

    public class TransactionTypeDescriptorsRepository : ITransactionTypeDescriptorsRepository
    {
        private readonly MyExpensesContext _context;

        public TransactionTypeDescriptorsRepository(MyExpensesContext context)
        {
            _context = context;
        }

        public async Task<IList<long>> CreateAll(IEnumerable<TransactionTypeDescriptor> entities)
        {
            await _context.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities.Select(x => x.TransactionTypeDescriptorId).ToList();
        }

        public void DropAll() =>
            _context.TransactionTypeDescriptors.Clear();

        public async Task<IList<TransactionTypeDescriptor>> Get() =>
            await _context.TransactionTypeDescriptors.ToListAsync();

        public async Task<TransactionTypeDescriptor> GetByDescription(string description) =>
            await _context.TransactionTypeDescriptors.FirstOrDefaultAsync(x => x.Description == description);

        public async Task<TransactionTypeDescriptor> GetFirst(Expression<Func<TransactionTypeDescriptor, bool>> predicate) =>
            await _context.TransactionTypeDescriptors.FirstOrDefaultAsync(predicate);
    }
}
