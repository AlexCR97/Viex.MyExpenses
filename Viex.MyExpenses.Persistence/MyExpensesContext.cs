using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Viex.MyExpenses.Persistence.Entities;

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
}
