using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Viex.MyExpenses.Persistence.Entities;

namespace Viex.MyExpenses.Persistence.Repositores.TransactionEntries
{
    public interface ITransactionEntriesRepository : IRepository<TransactionEntry>
    {
        Task DeleteAll();
        Task<IList<TransactionEntry>> Search(TransactionEntriesQueryParams queryParams);
    }

    public class TransactionEntriesRepository : ITransactionEntriesRepository
    {
        private readonly MyExpensesContext _context;

        public TransactionEntriesRepository(MyExpensesContext context)
        {
            _context = context;
        }

        public async Task<long> Create(TransactionEntry entity)
        {
            await _context.TransactionEntries.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.TransactionEntryId;
        }

        public async Task Delete(long id)
        {
            var snapshot = await _context.TransactionEntries.FirstAsync(s => s.TransactionEntryId == id);
            _context.TransactionEntries.Remove(snapshot);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAll()
        {
            _context.TransactionEntries.Clear();
            _context.SaveChangesAsync();
            return Task.CompletedTask;
        }

        public async Task<TransactionEntry> GetById(long id)
        {
            return await _context.TransactionEntries
                .Where(s => s.TransactionEntryId == id)
                .Include(s => s.Category)
                .Include(s => s.Type)
                .FirstOrDefaultAsync();
        }

        public async Task<TransactionEntry> GetFirst(Expression<Func<TransactionEntry, bool>> predicate)
        {
            return await _context.TransactionEntries
                .Where(predicate)
                .Include(s => s.Category)
                .Include(s => s.Type)
                .FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TransactionEntry>> GetWhere(Expression<Func<TransactionEntry, bool>> predicate)
        {
            return await _context.TransactionEntries
                .Where(predicate)
                .Include(s => s.Category)
                .Include(s => s.Type)
                .ToListAsync();
        }

        public async Task<IList<TransactionEntry>> Search(TransactionEntriesQueryParams queryParams)
        {
            var transactions = _context.TransactionEntries.AsQueryable();

            if (queryParams != null)
            {
                if (queryParams.DateCreatedFrom.HasValue)
                    transactions = transactions.Where(x => x.DateCreated >= queryParams.DateCreatedFrom);

                if (queryParams.DateCreatedTo.HasValue)
                    transactions = transactions.Where(x => x.DateCreated < queryParams.DateCreatedTo);
            }

            return await transactions
                .Include(x => x.Category)
                .Include(x => x.Type)
                .ToListAsync();
        }

        public async Task Update(TransactionEntry entity)
        {
            _context.TransactionEntries.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
