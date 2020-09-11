using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MarketFlight
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
                .UseMonitoring( "Monitoring" )
                .ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder
                    .UseUrls( "http://*:801" )
                    .UseStartup<Startup>();
				});
	}
}
