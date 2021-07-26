using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Viex.MyExpenses.Spa
{
    public static class Startup
    {
        public static IServiceCollection AddSpaLayer(this IServiceCollection services, SpaLayerConfigurations configuration)
        {
            services.AddControllersWithViews();
            
            services.AddMvc();
            
            services.AddSpaStaticFiles(config =>
            {
                config.RootPath = configuration.ServePath;
            });

            return services;
        }

        public static IApplicationBuilder UseSpaLayer(this IApplicationBuilder app, SpaLayerBuilderOptions options)
        {
            app.UseStaticFiles();

            app.UseSpaStaticFiles();

            app.UseSpa(config =>
            {
                config.Options.SourcePath = options.SourcePath;
            });

            return app;
        }
    }
}
