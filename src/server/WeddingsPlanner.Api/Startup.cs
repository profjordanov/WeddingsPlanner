using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using WeddingsPlanner.Api.Configuration;
using WeddingsPlanner.Api.Filters;
using WeddingsPlanner.Api.ModelBinders;
using WeddingsPlanner.Business.Generators;
using WeddingsPlanner.Business.Identity;
using WeddingsPlanner.Business.Services;
using WeddingsPlanner.Core.Configuration;
using WeddingsPlanner.Core.Generators;
using WeddingsPlanner.Core.Identity;
using WeddingsPlanner.Core.Services;
using WeddingsPlanner.Data.EntityFramework;

namespace WeddingsPlanner.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext(Configuration.GetConnectionString("DbConnectionString"));
            services.AddAutoMapper();
            services.AddSwagger();
            services.AddJwtIdentity(Configuration.GetSection(nameof(JwtConfiguration)));

            services.AddLogging(logBuilder => logBuilder.AddSerilog(dispose: true));

            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IAgenciesService, AgenciesService>();
            services.AddTransient<IOnboardingService, OnboardingService>();
            services.AddTransient<IVenuesService, VenuesService>();

            services.AddTransient<IJwtFactory, JwtFactory>();

            services.AddTransient<ICsvReportGenerator, CsvReportGenerator>();

            services.AddMvc(options =>
            {
                options.ModelBinderProviders.Insert(0, new OptionModelBinderProvider());
                options.Filters.Add<ExceptionFilter>();
                options.Filters.Add<ModelStateFilter>();
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ApplicationDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                dbContext.Database.EnsureCreated();
            }
            else
            {
                app.UseHsts();
            }

            loggerFactory.AddLogging(Configuration.GetSection("Logging"));

            app.UseHttpsRedirection();
            app.UseSwagger("My Web API.");
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
