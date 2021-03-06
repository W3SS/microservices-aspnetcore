using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace EcommerceInventory
{
    public class Program
    {
        static void Main(string[] args)
        {
			IConfiguration config = new ConfigurationBuilder()
							.AddCommandLine(args)
							.Build();

			var host = new WebHostBuilder()
						.UseKestrel()
						.UseStartup<Startup>()
						.UseConfiguration(config)
						.Build();

			host.Run();
        }
    }    
}
