using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using MSSqlWebapi.Models;

namespace MSSqlWebapi
{
    public class Program
    {
        public static int Main(string[] args)
        {
            // Initialize Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information("*** REST API for SQL Server (hosted on ASP.NET Core Web API)");
                TestSqlServerConnection();
                StartWebHost();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "*** Host terminated unexpectedly, examine exception.");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static void TestSqlServerConnection()
        {             
            Log.Information("Testing connection to SQL Server");
            var ctx = new ServerContext();
            Log.Information("Test connection successful. SQL Server version: {0}", ctx.SmoServer.VersionString);
        }

        private static void StartWebHost()
        {
            Log.Information("Starting Web Host");
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseIISIntegration()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();
            host.Run();
        }
    }
}