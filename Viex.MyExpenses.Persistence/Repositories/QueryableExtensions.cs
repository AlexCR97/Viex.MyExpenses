using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viex.MyExpenses.Persistence.Repositories.TransactionEntries;
using Viex.MyExpenses.Persistence.Repositories.Users;

namespace Viex.MyExpenses.Persistence.Repositories
{
    public static class QueryableExtensions
    {
        public static IQueryable<TransactionEntry> IncludeEverything(this IQueryable<TransactionEntry> query) => query
            .Include(x => x.TransactionCategoryDescriptor)
            .Include(x => x.TransactionSubCategoryDescriptor);
        
        public static IQueryable<User> IncludeEverything(this IQueryable<User> query) => query
            .Include(x => x.RoleDescriptor);
    }
}
