using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BillsMonster.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    // webBuilder.UseKestrel();
                    //webBuilder.ConfigureKestrel(options =>
                    //{
                    //    options.Listen(IPAddress.Loopback, 5001, listenOptions => {
                    //        listenOptions.UseHttps("something.pfx", "password")
                    //    });
                    //});
                });
    }
}
