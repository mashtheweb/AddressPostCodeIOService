using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Routing;

namespace AddressPostcodeIOService
{
    public class Startup
    {
        public static string ConnectionString { get; private set; }
        public static string AddressPostcodeServiceString { get; private set; }
        public static string AddressPostcodeServiceAPIKeyString { get; private set; }

        public Startup(IHostingEnvironment env)
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Set up configuration sources.

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.SetBasePath(env.ContentRootPath);
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            ConnectionString = Configuration["DBConnection"];
            AddressPostcodeServiceString = Configuration["AddressPostcodeServiceURL"];
            AddressPostcodeServiceAPIKeyString = Configuration["AddressPostcodeServiceAPIKey"];

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //loggerFactory.AddLog4Net("log4net.config", Configuration.GetSection("Log4net"));
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            app.UseMvc();

            RegisterRoutes(app);

        }

        public static string GetDefaultConnectionString()
        {
            return Startup.ConnectionString;
        }

        public void RegisterRoutes(IApplicationBuilder app)
        {
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});

        }
    }
}
