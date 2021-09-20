using Microsoft.EntityFrameworkCore;
using Viex.MyExpenses.Persistence.Repositories.TransactionCategoryDescriptors;
using Viex.MyExpenses.Persistence.Repositories.RoleDescriptors;
using Viex.MyExpenses.Persistence.Repositories.TransactionEntries;
using Viex.MyExpenses.Persistence.Repositories.TransactionTypeDescriptors;
using Viex.MyExpenses.Persistence.Repositories.Users;
using Viex.MyExpenses.Persistence.Repositories.TransactionSubCategoryDescriptors;

namespace Viex.MyExpenses.Persistence
{
    public class MyExpensesContext : DbContext
    {
        public MyExpensesContext(DbContextOptions<MyExpensesContext> options)
            : base(options) { }

        #region Descriptors
        public DbSet<TransactionCategoryDescriptor> TransactionCategoryDescriptors { get; set; }
        public DbSet<TransactionSubCategoryDescriptor> TransactionSubCategoryDescriptors { get; set; }
        public DbSet<TransactionTypeDescriptor> TransactionTypeDescriptors { get; set; }
        public DbSet<RoleDescriptor> RoleDescriptors { get; set; }
        #endregion

        #region Standalone
        public DbSet<TransactionEntry> TransactionEntries { get; set; }
        public DbSet<User> Users { get; set; }
        #endregion

        #region Associations

        #endregion
    }

    public static class DbContextExtensions
    {
        public static void Clear<T>(this DbSet<T> dbSet) where T : class =>
            dbSet.RemoveRange(dbSet);
    }
}
