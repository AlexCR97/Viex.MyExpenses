using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Viex.MyExpenses.Core.Extensions;
using Viex.MyExpenses.Persistence;

namespace Viex.MyExpenses.Domain
{
    public static class Startup
    {
        public static IServiceCollection AddDomainLayer(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddByConvention<IDomainLayerMarker>("I", "Context")
                .AddByConvention<IDomainLayerMarker>("I", "Mapper")
                .AddByConvention<IDomainLayerMarker>("I", "Provider")
                .AddByConvention<IDomainLayerMarker>("I", "Service")
                .AddPersistenceLayer(configuration)
                ;
        }
    }
}
