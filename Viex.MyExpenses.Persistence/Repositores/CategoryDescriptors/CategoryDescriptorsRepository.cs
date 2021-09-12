using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Viex.MyExpenses.Persistence.Repositores.CategoryDescriptors
{
    public interface ICategoryDescriptorsRepository : IDescriptorRepository<CategoryDescriptor>
    {

    }

    public class CategoryDescriptorsRepository : ICategoryDescriptorsRepository
    {
        private readonly MyExpensesContext _context;

        public CategoryDescriptorsRepository(MyExpensesContext context)
        {
            _context = context;
        }

        public async Task<IList<long>> CreateAll(IEnumerable<CategoryDescriptor> entities)
        {
            await _context.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities.Select(x => x.CategoryDescriptorId).ToList();
        }

        public void DropAll() =>
            _context.CategoryDescriptors.Clear();

        public async Task<IList<CategoryDescriptor>> Get() =>
            await _context.CategoryDescriptors.ToListAsync();

        public async Task<CategoryDescriptor> GetByDescription(string description) =>
            await _context.CategoryDescriptors.FirstOrDefaultAsync(x => x.Description == description);

        public async Task<CategoryDescriptor> GetFirst(Expression<Func<CategoryDescriptor, bool>> predicate) =>
            await _context.CategoryDescriptors.FirstOrDefaultAsync(predicate);
    }
}
