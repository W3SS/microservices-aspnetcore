using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;

namespace SecureWebApp
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)                
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddOptions();

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<IdentityDbContext>();

            services.AddAuthentication(options =>
                    {
                        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                    })
                .AddCookie()
                .AddOpenIdConnect(options =>
                    {
                        options.Authority = $"https://{this.Configuration["OpenID:Domain"]}"; ;
                        options.ClientId = this.Configuration["OpenID:ClientId"];
                        options.ClientSecret = this.Configuration["OpenID:ClientSecret"];

                        options.Scope.Clear();
                        options.Scope.Add("openid");
                        options.Scope.Add("name");
                        options.Scope.Add("email");
                        options.Scope.Add("picture");

                        options.ResponseType = "code";
                        options.CallbackPath = new PathString("/signin-auth0");

                        options.ClaimsIssuer = "Auth0";
                        options.SaveTokens = true;
                        options.Events = CreateOpenIdConnectEvents();
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
                    ILoggerFactory loggerFactory,
                    IOptions<OpenIDSettings> openIdSettings)
        {

            Console.WriteLine("Using OpenID Auth domain of : " + openIdSettings.Value.Domain);
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static OpenIdConnectEvents CreateOpenIdConnectEvents()
        {
            return new OpenIdConnectEvents
            {
                OnTicketReceived = context =>
                {
                    if (context.Principal.Identity is ClaimsIdentity identity) {
                        if (!context.Principal.HasClaim( c => c.Type == ClaimTypes.Name) &&
                        identity.HasClaim( c => c.Type == "name"))
                        identity.AddClaim(new Claim(ClaimTypes.Name, identity.FindFirst("name").Value));
                    }
                    return Task.FromResult(0);
                }
            };
        }
    }
}
