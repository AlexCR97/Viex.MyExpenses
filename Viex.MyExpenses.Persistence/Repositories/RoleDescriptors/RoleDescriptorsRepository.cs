using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Viex.MyExpenses.Persistence.Repositories.RoleDescriptors
{
    public interface IRoleDescriptorsRepository : IDescriptorRepository<RoleDescriptor>
    {

    }

    public class RoleDescriptorsRepository : IRoleDescriptorsRepository
    {
        private readonly MyExpensesContext _context;

        public RoleDescriptorsRepository(MyExpensesContext context)
        {
            _context = context;
        }

        public async Task<IList<int>> CreateAll(IEnumerable<RoleDescriptor> entities)
        {
            await _context.RoleDescriptors.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities.Select(x => x.RoleDescriptorId).ToList();
        }

        public void DropAll() =>
            _context.RoleDescriptors.Clear();

        public async Task<IList<RoleDescriptor>> Get() =>
            await _context.RoleDescriptors.ToListAsync();

        public async Task<RoleDescriptor> GetByDescription(string description) =>
            await _context.RoleDescriptors
            .Where(x => x.Description == description)
            .FirstOrDefaultAsync();

        public async Task<RoleDescriptor> GetFirst(Expression<Func<RoleDescriptor, bool>> predicate) =>
            await _context.RoleDescriptors
            .Where(predicate)
            .FirstOrDefaultAsync();
    }
}
