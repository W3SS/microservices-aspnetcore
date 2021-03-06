using System.IO;

using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace Secureservice 
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                    .AddCommandLine(args)
                    .Build();

                var host = new WebHostBuilder()
                    .UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())                
                    .UseStartup<Startup>()
                    .UseConfiguration(config)
                    .Build();

                host.Run();
        }
    }
}
