using System;

using Grpc.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PartialFoods.Services.APIServer
{
    public class Startup
    {
        private readonly ILogger logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            this.Configuration = configuration;
            this.logger = logger;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("CONFIGURE SERVICES CALLED");
            // TODO: make these channels DNS-disco-friendly
            var cmdChannel = new Channel(this.Configuration["rpcclient:ordercommand"], ChannelCredentials.Insecure);
            var client = new OrderCommand.OrderCommandClient(cmdChannel);

            var invChannel = new Channel(this.Configuration["rpcclient:inventory"], ChannelCredentials.Insecure);
            var invClient = new InventoryManagement.InventoryManagementClient(invChannel);

            var orderChannel = new Channel(this.Configuration["rpcclient:ordermanagement"], ChannelCredentials.Insecure);
            var orderClient = new OrderManagement.OrderManagementClient(orderChannel);

            this.logger.LogInformation($"Order Command Client: {cmdChannel.ResolvedTarget}");
            this.logger.LogInformation($"Order Management Client: {orderChannel.ResolvedTarget}");
            this.logger.LogInformation($"Inventory Client: {invChannel.ResolvedTarget}");

            services.AddSingleton(typeof(OrderCommand.OrderCommandClient), client);
            services.AddSingleton(typeof(InventoryManagement.InventoryManagementClient), invClient);
            services.AddSingleton(typeof(OrderManagement.OrderManagementClient), orderClient);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
