using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viex.MyExpenses.Core.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddByConvention<TMarker>(this IServiceCollection services, string prefix, string suffix)
        {
            var types = typeof(TMarker).Assembly.ExportedTypes;

            var servicesToRegister = (
                from interfaceType in types.Where(t => t.Name.StartsWith(prefix) && t.Name.EndsWith(suffix))
                from serviceType in types.Where(t => t.Name == interfaceType.Name.Substring(1))
                select new
                {
                    InterfaceType = interfaceType,
                    ServiceType = serviceType
                }
            );

            foreach (var pair in servicesToRegister)
                services.AddScoped(pair.InterfaceType, pair.ServiceType);

            return services;
        }
    }
}
