using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Viex.MyExpenses.Persistence
{
    public static class Startup
    {
        public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyExpensesContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("Default");
                options.UseSqlServer(connectionString);
            });

            return services;
        }
    }
}
