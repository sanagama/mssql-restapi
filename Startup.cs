using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using MSSqlWebapi.Models;

namespace MSSqlWebapi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Get SQL host, username and password from environment variables
            var hostname = Environment.GetEnvironmentVariable("SQLSERVER_HOST") ?? "127.0.0.1";
            var username = Environment.GetEnvironmentVariable("SQLSERVER_USERNAME") ?? "sa";
            var password = Environment.GetEnvironmentVariable("SQLSERVER_PASSWORD") ?? "Yukon900";

            // Build connection string
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = hostname;
            builder.UserID = username;
            builder.Password = password;
            builder.InitialCatalog = "master";

            // Add Entitry Framework services.
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<ApiContext>(options => options.UseSqlServer(builder.ConnectionString));

            // Add MVC
            services.AddMvc().AddJsonOptions(options =>
            {
                // handle loops correctly
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                // use standard name conversion of properties
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
