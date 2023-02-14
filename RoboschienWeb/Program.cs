
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace RoboschienWebFF
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
           // CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseKestrel(options => options.AddServerHeader = false)
            .ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                logging.AddDebug(); logging.AddNLog();
            })
            .UseStartup<Startup>();


        //public static IHostBuilder CreateHostBuilder(string[] args) => 
            
        //    Host.CreateDefaultBuilder(args).ConfigureLogging((hostingContext, logging) => {
        //        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
        //        logging.AddDebug(); logging.AddNLog();
        //    })
        //    .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
