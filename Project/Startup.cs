using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Project
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddRouting(c => {
                c.AppendTrailingSlash = false;
                c.LowercaseUrls = true;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            { 
                routes.MapRoute(
                  name: "home",
                  template: "",
                  defaults: new { controller = "Home", Action = "Index" }
                );

                routes.MapRoute(
                    name: "about",
                    template: "about/{language}",
                    defaults: new { language = "en", controller = "Home", Action = "About" }
                );

                //routes.MapRoute(
                //  name: "default5",
                //  template: "{controller?}/{urlTitle?}",
                //  defaults: new { amp = "", language = "en", controller = "Home", Action = "Index" }
                //);

                //routes.MapRoute(
                //   name: "default4",
                //   template: "{language:regex(^en|cn$)}/{controller?}/{urlTitle?}",
                //   defaults: new { amp = "", controller = "Home", Action = "Index" }
                //);

                //routes.MapRoute(
                //    name: "default9",
                //    template: "{amp:regex(^amp$)}/{controller?}/{urlTitle?}",
                //    defaults: new { language = "en", controller = "Home", Action = "Index" }
                //);

                //routes.MapRoute(
                //     name: "default6",
                //     template: "{amp:regex(^amp$)}/{language:regex(^en|cn$)}/{controller?}/{urlTitle?}",
                //     defaults: new { controller = "Home", Action = "Index" }
                //);



                //routes.MapRoute(
                //    name: "default1",
                //    template: "{amp:regex(^amp$)}/{language:regex(^en|cn$)}/{controller?}/{urlTitle?}",
                //    defaults: new { contoller = "Home", Action = "Index" }
                //);

                //routes.MapRoute(
                //    name: "default2",
                //    template: "{amp:regex(^amp$)}/{language:regex(^en|cn$)?}/{controller?}/{urlTitle?}",
                //    defaults: new { language = "en", controller = "Home", Action = "Index" }
                //);

                //routes.MapRoute(
                //    name: "default3",
                //    template: "{language:regex(^en|cn$)}/{controller?}/{urlTitle?}",
                //    defaults: new { controller = "Home", Action = "Index" }
                //);

                //routes.MapRoute(
                //  name: "default4",
                //  template: "{controller?}/{urlTitle?}",
                //  defaults: new { language = "en", controller = "Home", Action = "Index" }
                //);


            });
        }
    }
}
