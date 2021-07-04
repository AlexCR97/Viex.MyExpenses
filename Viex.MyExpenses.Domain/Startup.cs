using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Viex.MyExpenses.Domain.Services;
using Viex.MyExpenses.Persistence;

namespace Viex.MyExpenses.Domain
{
    public static class Startup
    {
        public static IServiceCollection AddDomainLayer(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddScoped<IDescriptorService, DescriptorService>()
                .AddPersistenceLayer(configuration)
                ;
        }
    }
}
