using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Viex.MyExpenses.Persistence.Repositores.Users
{
    public interface IUsersRepository : IRepository<User>
    {

    }

    public class UsersRepository : IUsersRepository
    {
        private readonly MyExpensesContext _context;

        public UsersRepository(MyExpensesContext context)
        {
            _context = context;
        }

        public async Task<long> Create(User entity)
        {
            entity.DateCreated = DateTime.Now;
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.UserId;
        }

        public async Task Delete(long id)
        {
            var snapshot = await _context.Users.FirstAsync(s => s.UserId == id);
            _context.Users.Remove(snapshot);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetById(long id)
        {
            return await _context.Users.FirstOrDefaultAsync(s => s.UserId == id);
        }

        public async Task<User> GetFirst(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users.FirstOrDefaultAsync(predicate);
        }

        public async Task<IList<User>> GetWhere(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users
                .Where(predicate)
                .ToListAsync();
        }

        public async Task Update(User entity)
        {
            var snapshot = await _context.Users.FirstAsync(s => s.UserId == entity.UserId);
            snapshot.DateUpdated = DateTime.Now;
            snapshot.FirstName = entity.FirstName;
            snapshot.LastName = entity.LastName;
            snapshot.UserName = entity.UserName;

            _context.Users.Update(snapshot);
            await _context.SaveChangesAsync();
        }
    }
}
