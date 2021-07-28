using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Viex.MyExpenses.Domain.Contexts.Session;
using Viex.MyExpenses.Domain.Providers.Csv;
using Viex.MyExpenses.Domain.Services;
using Viex.MyExpenses.Domain.Services.Authentication;
using Viex.MyExpenses.Domain.Services.TransactionEntries;
using Viex.MyExpenses.Domain.Services.Users;
using Viex.MyExpenses.Persistence;

namespace Viex.MyExpenses.Domain
{
    public static class Startup
    {
        public static IServiceCollection AddDomainLayer(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddScoped<IAuthenticationService, AuthenticationService>()
                .AddScoped<IDescriptorService, DescriptorService>()
                .AddScoped<ITransactionEntryService, TransactionEntryService>()
                .AddScoped<IUserService, UserService>()
                .AddTransient<ICsvProvider, CsvProvider>()
                .AddTransient<ISessionContext, SessionContext>()
                .AddPersistenceLayer(configuration)
                ;
        }
    }
}
