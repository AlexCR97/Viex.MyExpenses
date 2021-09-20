using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Viex.MyExpenses.Persistence.Repositories.Users
{
    public interface IUsersRepository : IRepository<User, int>
    {

    }

    public class UsersRepository : IUsersRepository
    {
        private readonly MyExpensesContext _context;

        public UsersRepository(MyExpensesContext context)
        {
            _context = context;
        }

        public async Task<int> Create(User entity)
        {
            entity.DateCreated = DateTime.Now;
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.UserId;
        }

        public async Task Delete(int id)
        {
            var snapshot = await _context.Users.FirstAsync(s => s.UserId == id);
            _context.Users.Remove(snapshot);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetById(int id) =>
            await _context.Users
            .IncludeEverything()
            .FirstOrDefaultAsync(s => s.UserId == id);

        public async Task<User> GetFirst(Expression<Func<User, bool>> predicate) =>
            await _context.Users
            .IncludeEverything()
            .FirstOrDefaultAsync(predicate);

        public async Task<IList<User>> GetWhere(Expression<Func<User, bool>> predicate) =>
            await _context.Users
            .Where(predicate)
            .IncludeEverything()
            .ToListAsync();

        public async Task Update(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
