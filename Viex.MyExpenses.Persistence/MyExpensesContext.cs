using Microsoft.EntityFrameworkCore;
using Viex.MyExpenses.Persistence.Repositores.CategoryDescriptors;
using Viex.MyExpenses.Persistence.Repositores.TransactionEntries;
using Viex.MyExpenses.Persistence.Repositores.TransactionTypeDescriptors;
using Viex.MyExpenses.Persistence.Repositores.Users;

namespace Viex.MyExpenses.Persistence
{
    public class MyExpensesContext : DbContext
    {
        public MyExpensesContext(DbContextOptions<MyExpensesContext> options)
            : base(options) { }

        public DbSet<CategoryDescriptor> CategoryDescriptors { get; set; }
        public DbSet<TransactionEntry> TransactionEntries { get; set; }
        public DbSet<TransactionTypeDescriptor> TransactionTypeDescriptors { get; set; }
        public DbSet<User> Users { get; set; }
    }

    public static class DbContextExtensions
    {
        public static void Clear<T>(this DbSet<T> dbSet) where T : class =>
            dbSet.RemoveRange(dbSet);
    }
}
