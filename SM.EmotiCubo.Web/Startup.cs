using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;

namespace SM.EmotiCubo.Web
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
			services.Configure<AppSettings>(Configuration.GetSection("AppSettings")); // http://edi.wang/post/2016/10/9/read-appsettings-aspnet-core

            services.AddMvc(o => {
                o.EnableEndpointRouting = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            /*
            app.UseEndpoints(
                endpoints =>
				{
                    endpoints.MapControllerRoute(
						name: "default",
						pattern: "{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapFallbackToController("Index", "Home");
                    // endpoints.MapControllerRoute(name: "test", pattern: "Index.html");
                    //endpoints.AddControllersWithViews();
                }
            );
            app.AddControllersWithViews();
            */
            app.UseMvc();
			app.UseDefaultFiles(); // https://stackoverflow.com/questions/43090718/setting-index-html-as-default-page-in-asp-net-core
			app.UseStaticFiles();
        }
    }
}
