using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using LocationReporter.Models;
using LocationReporter.Events;
using LocationReporter.Services;

namespace LocationReporter
{
    public class Startup
    {        
        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory) 
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();
            
            var builder = new ConfigurationBuilder()                
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
		        .AddEnvironmentVariables();

            this.Configuration = builder.Build();    		        
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services) 
        {
            services.AddMvc();
            services.AddOptions();

            services.Configure<AMQPOptions>(this.Configuration.GetSection("amqp"));            
            services.Configure<TeamServiceOptions>(this.Configuration.GetSection("teamservice"));

            services.AddSingleton(typeof(IEventEmitter), typeof(AMQPEventEmitter));
            services.AddSingleton(typeof(ICommandEventConverter), typeof(CommandEventConverter));
            services.AddSingleton(typeof(ITeamServiceClient), typeof(HttpTeamServiceClient));
        }

        public void Configure(IApplicationBuilder app, 
                IHostingEnvironment env, 
                ILoggerFactory loggerFactory,
                ITeamServiceClient teamServiceClient,
                IEventEmitter eventEmitter) 
        {           
            // Asked for instances of singletons during Startup
            // to force initialization early.
            
            app.UseMvc();
        }
    }
}

