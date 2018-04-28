using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Secureservice
{
    using System;

    using Microsoft.AspNetCore.Authentication.JwtBearer;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)                
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {                                   
            services.AddMvc();
            services.AddOptions();

            services.AddAuthorization(
                options =>
                    {
                        options.AddPolicy(
                            "CheeseburgerPolicy",
                            policy => policy.RequireClaim("icanhazcheeseburger", "true"));
                    });

            var secretKey = "seriouslyneverleavethissittinginyourcode"; //this.Configuration.GetSection("TokenProviderOptions:SecretKey").Value)
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var tokenValidationParameters = new TokenValidationParameters
                                                {
                                                    RequireExpirationTime = true,
                                                    RequireSignedTokens = true,
                                                    ValidateIssuerSigningKey = true,
                                                    IssuerSigningKey = signingKey,
                                                    ValidateIssuer = false,
                                                    ValidIssuer = "https://fake.issuer.com",//this.Configuration.GetSection("TokenProviderOptions:Issuer").Value,
                                                    ValidateAudience = false,
                                                    ValidAudience = "https://sampleservice.example.com",//this.Configuration.GetSection("TokenProviderOptions:Audience").Value,
                                                    ValidateLifetime = true,
                                                    ClockSkew = TimeSpan.Zero
                                                };

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(options =>
                {
                    options.Audience = this.Configuration.GetSection("TokenProviderOptions:Audience").Value;
                    options.ClaimsIssuer = this.Configuration.GetSection("TokenProviderOptions:Issuer").Value;
                    options.TokenValidationParameters = tokenValidationParameters;
                    options.SaveToken = true;
                });
        }

         public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
                                ILoggerFactory loggerFactory)
         {
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
         }

        public IConfigurationRoot Configuration { get; }

    }
}