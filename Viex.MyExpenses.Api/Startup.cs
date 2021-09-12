using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using Viex.MyExpenses.Domain;

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
                .AddHttpContextAccessor()
                .AddCustomAuthentication(Configuration)
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
                    "http://localhost:8080", // Vue SPA
                    "http://localhost:4200", // Angular SPA
                    "https://myexpensesstorage.z19.web.core.windows.net", // Hosted SPA
                };

                options
                    .WithOrigins(allowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    ;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public static class StartupExtensions
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration )
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = configuration["Authentication:Audience"],
                        ValidIssuer = configuration["Authentication:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:Key"]))
                    };
                });

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
            });

            return services;
        }

        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    var interceptedError = context.Features.Get<IExceptionHandlerFeature>();

                    if (interceptedError == null || interceptedError.Error == null)
                        return;

                    var caughtException = interceptedError.Error.InnerException ?? interceptedError.Error;

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
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public int StatusCode { get; set; }

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
                StackTrace = ex.StackTrace,
                StatusCode = (int)statusCode,
            };
        }
    }
}
