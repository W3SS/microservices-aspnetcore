namespace locationservice {
    using System;

    using locationservice.Models;
    using locationservice.Repositories;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class Startup
    {
        public static string[] Args {get; set;} = {};
        private readonly ILogger logger;
        private readonly ILoggerFactory loggerFactory;

        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional:true)
                .AddEnvironmentVariables()
                .AddCommandLine(Startup.Args);                

            Configuration = builder.Build();

            this.loggerFactory = loggerFactory;
            this.loggerFactory.AddConsole(LogLevel.Information);
            this.loggerFactory.AddDebug();

            this.logger = this.loggerFactory.CreateLogger("Startup");
        }

        public static IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {                                    
            //var transient = Boolean.Parse(Configuration.GetSection("transient").Value);
            var transient = true;
            if (Configuration.GetSection("transient") != null) {
                transient = Boolean.Parse(Configuration.GetSection("transient").Value);
            }
            if (transient) {
                this.logger.LogInformation("Using transient location record repository.");
                services.AddScoped<ILocationRecordRepository, InMemoryLocationRecordRepository>();
            } else {                
                var connectionString = Configuration.GetSection("postgres:cstr").Value;                        
                services.AddEntityFrameworkNpgsql().AddDbContext<LocationDbContext>(options =>
                    options.UseNpgsql(connectionString));
                this.logger.LogInformation("Using '{0}' for DB connection string.", connectionString);
                services.AddScoped<ILocationRecordRepository, LocationRecordRepository>();
            }
            
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
