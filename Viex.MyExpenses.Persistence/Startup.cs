using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Viex.MyExpenses.Core.Extensions;

namespace Viex.MyExpenses.Persistence
{
    public static class Startup
    {
        public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddByConvention<IPersistenceLayerMarker>("I", "Repository")
                .AddDbContext<MyExpensesContext>(options =>
                {
                    var connectionString = configuration.GetConnectionString("Default");
                    options.UseSqlServer(connectionString);
                });
        }
    }
}
