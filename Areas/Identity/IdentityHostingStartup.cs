using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProDat.Web2.Areas.Identity.Data;
using ProDat.Web2.Areas.Identity.Services;
using ProDat.Web2.Data;

[assembly: HostingStartup(typeof(ProDat.Web2.Areas.Identity.IdentityHostingStartup))]
namespace ProDat.Web2.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            // ######################################################################
            // Customisation of scaffolding can be done based on this documentation:
            // https://docs.microsoft.com/en-gb/aspnet/core/security/authentication/scaffold-identity?view=aspnetcore-3.1&tabs=visual-studio#password-configuration
            //            
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<IdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("IdentityContextConnection")));

                services.AddSession(options =>
                {
                    options.IdleTimeout = TimeSpan.FromMinutes(60);
                    options.Cookie.MaxAge = TimeSpan.FromHours(3);
                    options.Cookie.Name = "PDSessionCookie";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.IsEssential = true;
                    options.Cookie.Expiration = TimeSpan.FromHours(3);
                });

                services.AddDefaultIdentity<ProDatWeb2User>(options => options.SignIn.RequireConfirmedAccount = true)
                    // added roles (new)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<IdentityContext>();

                // ###########################################
                // added email functionality for Area/Identity
                services.AddTransient<IEmailSender, EmailSender>();
                services.Configure<AuthMessageSenderOptions>(context.Configuration);

                services.Configure<IdentityOptions>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequiredUniqueChars = 4;
                });

                services.ConfigureApplicationCookie(options =>
                {
                    options.Cookie.MaxAge = TimeSpan.FromHours(3);
                    options.Cookie.Name = "PDAppCookie";
                    options.Cookie.HttpOnly = true;
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Identity/Account/Login");
                    options.SlidingExpiration = true;

                });

                // set fallback authorisation
                services.AddAuthorization(options =>
                {
                    options.AddPolicy("TwoFactorEnabled", 
                        x => x.RequireClaim("amr", "mfa"));

                    // only users that have logged in can access website
                    options.FallbackPolicy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();

                    
                });
            });
        }
    }
}