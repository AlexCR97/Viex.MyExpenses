using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using Viex.MyExpenses.Domain;
using Viex.MyExpenses.Spa;

namespace Viex.MyExpenses.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDomainLayer(Configuration)
                //.AddSpaLayer(SpaLayerConfigurations)
                .AddControllers()
                ;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomExceptionHandler();

            app.UseHttpsRedirection();

            //app.UseSpaLayer(SpaLayerBuilderOptions);

            app.UseCors(options =>
            {
                var allowedOrigins = new string[]
                {
                    "http://localhost:8080"
                };

                options
                    .WithOrigins(allowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    ;
            });

            app.UseRouting();

            //app.UseAuthorization(); // TODO Add auth

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private string SolutionPath
        {
            get {
                var combined = Path.Combine(Environment.ContentRootPath, @"..\");
                return Path.GetFullPath(combined);
            }
        }

        private string SpaProjectPath
        {
            get {
                return $"{SolutionPath}/Viex.MyExpenses.Spa";
            }
        }

        private SpaLayerConfigurations SpaLayerConfigurations
        {
            get {
                return new SpaLayerConfigurations
                {
                    ServePath = $"{SpaProjectPath}/ClientApp/public",
                };
            }
        }

        private SpaLayerBuilderOptions SpaLayerBuilderOptions
        {
            get {
                return new SpaLayerBuilderOptions
                {
                    SourcePath = $"{SpaProjectPath}/ClientApp",
                };
            }
        }
    }

    public static class StartupExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    var interceptedError = context.Features.Get<IExceptionHandlerFeature>();

                    if (interceptedError == null || interceptedError.Error == null)
                        return;

                    var caughtException = interceptedError.Error.InnerException == null
                        ? interceptedError.Error
                        : interceptedError.Error.InnerException;

                    var response = HttpErrorResponse.FromException(caughtException);
                    context.Response.StatusCode = response.StatusCode;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(response.ToString(), Encoding.UTF8);
                });
            });

            return app;
        }
    }

    public class HttpErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static HttpErrorResponse FromException(Exception ex)
        {
            var statusCode = HttpStatusCode.InternalServerError;

            if (ex is DomainException)
                statusCode = (ex as DomainException).StatusCode;

            return new HttpErrorResponse
            {
                Message = ex.Message,
                StatusCode = (int)statusCode,
            };
        }
    }
}
