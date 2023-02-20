using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProDat.Web2.Data;
using ProDat.Web2.TagLibrary;

namespace ProDat.Web2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TagContext>(options =>
               options
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                //.EnableSensitiveDataLogging(true)
               );

            // alter all sessions to timeout with 5 min.
            // see url to increase timeout for a specific controller:
            // https://stackoverflow.com/questions/58889857/add-session-timeout-from-controller-in-asp-net-core-mvc
            services.AddSession(options =>
            {
                // Modified timeout to be 8 hours MWM
                options.IdleTimeout = TimeSpan.FromSeconds(28800);
            });

            services.AddControllersWithViews().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // UseSession must be after UseRouting() and before MapRazorPages() and MapDefaultControllerRoute
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    //pattern: "{controller=Reports}/{action=Index}/{id?}"
                    //pattern: "{controller=UC3}/{action=Index}/{id?}"
                    pattern: "{controller=UC1}/{action=Index}/{id?}"
                    //pattern: "{controller=TestGrid}/{action=Index}/{id?}"
                );


                // added for scaffolded Identity Management
                endpoints.MapRazorPages();
            });
        }
    }
}
