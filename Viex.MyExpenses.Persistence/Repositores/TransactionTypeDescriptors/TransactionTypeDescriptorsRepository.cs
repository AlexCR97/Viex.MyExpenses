using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Viex.MyExpenses.Persistence.Entities;

namespace Viex.MyExpenses.Persistence.Repositores.TransactionTypeDescriptors
{
    public interface ITransactionTypeDescriptorsRepository : IRepository<TransactionTypeDescriptor>
    {

    }

    public class TransactionTypeDescriptorsRepository : ITransactionTypeDescriptorsRepository
    {
        private readonly MyExpensesContext _context;

        public TransactionTypeDescriptorsRepository(MyExpensesContext context)
        {
            _context = context;
        }

        public async Task<long> Create(TransactionTypeDescriptor entity)
        {
            entity.DateCreated = DateTime.Now;
            await _context.TransactionTypeDescriptors.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.TransactionTypeDescriptorId;
        }

        public async Task Delete(long id)
        {
            var snapshot = await _context.TransactionTypeDescriptors.FirstAsync(s => s.TransactionTypeDescriptorId == id);
            _context.TransactionTypeDescriptors.Remove(snapshot);
            await _context.SaveChangesAsync();
        }

        public async Task<TransactionTypeDescriptor> GetById(long id)
        {
            return await _context.TransactionTypeDescriptors.FirstOrDefaultAsync(s => s.TransactionTypeDescriptorId == id);
        }

        public async Task<TransactionTypeDescriptor> GetFirst(Expression<Func<TransactionTypeDescriptor, bool>> predicate)
        {
            return await _context.TransactionTypeDescriptors.FirstOrDefaultAsync(predicate);
        }

        public async Task<IList<TransactionTypeDescriptor>> GetWhere(Expression<Func<TransactionTypeDescriptor, bool>> predicate)
        {
            return await _context.TransactionTypeDescriptors
                .Where(predicate)
                .ToListAsync();
        }

        public Task Update(TransactionTypeDescriptor entity)
        {
            throw new NotImplementedException();
        }
    }
}
