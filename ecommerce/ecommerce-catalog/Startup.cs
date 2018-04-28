using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Logging;
using System.Linq;
using EcommerceCatalog.Persistence;
using Steeltoe.Discovery.Client;
using EcommerceCatalog.InventoryClient;

namespace EcommerceCatalog
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            this.Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDiscoveryClient(this.Configuration);
            services.AddScoped<IInventoryClient, HttpInventoryClient>();
            services.AddScoped<IProductRepository, MemoryProductRepository>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDiscoveryClient();
            app.UseMvc();
        }
    }
}