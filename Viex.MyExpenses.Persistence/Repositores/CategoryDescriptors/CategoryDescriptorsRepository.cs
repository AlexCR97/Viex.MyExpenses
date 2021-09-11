using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Viex.MyExpenses.Persistence.Repositores.CategoryDescriptors
{
    public interface ICategoryDescriptorsRepository : IRepository<CategoryDescriptor>
    {

    }

    public class CategoryDescriptorsRepository : ICategoryDescriptorsRepository
    {
        private readonly MyExpensesContext _context;

        public CategoryDescriptorsRepository(MyExpensesContext context)
        {
            _context = context;
        }

        public async Task<long> Create(CategoryDescriptor entity)
        {
            entity.DateCreated = DateTime.Now;
            await _context.CategoryDescriptors.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.CategoryDescriptorId;
        }

        public async Task Delete(long id)
        {
            var snapshot = await _context.CategoryDescriptors.FirstAsync(s => s.CategoryDescriptorId == id);
            _context.CategoryDescriptors.Remove(snapshot);
            await _context.SaveChangesAsync();
        }

        public async Task<CategoryDescriptor> GetById(long id)
        {
            return await _context.CategoryDescriptors.FirstOrDefaultAsync(s => s.CategoryDescriptorId == id);
        }

        public async Task<CategoryDescriptor> GetFirst(Expression<Func<CategoryDescriptor, bool>> predicate)
        {
            return await _context.CategoryDescriptors.FirstOrDefaultAsync(predicate);
        }

        public async Task<IList<CategoryDescriptor>> GetWhere(Expression<Func<CategoryDescriptor, bool>> predicate)
        {
            return await _context.CategoryDescriptors
                .Where(predicate)
                .ToListAsync();
        }

        public Task Update(CategoryDescriptor entity)
        {
            throw new NotImplementedException();
        }
    }
}
