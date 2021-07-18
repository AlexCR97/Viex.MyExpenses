using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Viex.MyExpenses.Persistence.Repositores;
using Viex.MyExpenses.Persistence.Repositores.TransactionEntries;

namespace Viex.MyExpenses.Persistence
{
    public static class Startup
    {
        public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddScoped<ICategoryDescriptorsRepository, CategoryDescriptorsRepository>()
                .AddScoped<ITransactionEntriesRepository, TransactionEntriesRepository>()
                .AddScoped<ITransactionTypeDescriptorsRepository, TransactionTypeDescriptorsRepository>()
                .AddScoped<IUsersRepository, UsersRepository>()
                .AddDbContext<MyExpensesContext>(options =>
                {
                    var connectionString = configuration.GetConnectionString("Default");
                    options.UseSqlServer(connectionString);
                })
                ;
        }
    }
}
