using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(path: @"c:\hottellistings\logs\log-.txt",
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.ff zz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: LogEventLevel.Information
                ).CreateLogger();

            try 
            {
                Log.Information("Application Is Starting");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {

                Log.Fatal(ex,"Application Failed To start");
            }
            finally
            {
                Log.CloseAndFlush();
            }

            
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>();
    }
}
