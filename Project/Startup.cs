using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Project.Module.Language;
using Project.Module.Amp;

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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new FeatureLocationExpander());
            });


            services.AddAmp();
            services.AddLanguage(options =>
            {
                options.DefaultLanguage = new Language
                {
                    ISOCode = "zh-Hans",
                    FriendlyISOCode = "中文"
                };
                options.SupportedLanguages = new List<Language> {
                    new Language {
                        ISOCode = "en",
                        FriendlyISOCode = "english"
                    },
                    new Language {
                        ISOCode = "zh-Hans",
                        FriendlyISOCode = "中文"
                    }
                };
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            IServiceProvider serviceProvider
        )
        {
            app.UseLanguage(serviceProvider);

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

            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: "aboutWithLanguage",
                //    template: "{language:language}/about-us",
                //    defaults: new { Controller = "Category", Action = "Index" }
                //);

                //routes.MapRoute(
                //    name: "about",
                //    template: "about-us",
                //    defaults: new { language = "zh-Hans", Controller = "Category", Action = "Index" }
                //);

                //routes.MapRoute(
                //    name: "home",
                //    template: "",
                //    defaults: new { Controller = "Home", Action = "Index" }
                // );

                 
                routes.MapRoute(
                   name: "AboutWithLanguageAmp",
                   template: "{amp:amp}/{language:language}/about-us",
                   defaults: new { Controller = "AboutUs", Action = "Index" }
                );

                routes.MapRoute(
                    name: "AboutWithLanguage",
                    template: "{language:language}/about-us",
                    defaults: new { Controller = "AboutUs", Action = "Index" }
                );

                routes.MapRoute(
                    name: "AboutWithAmp",
                    template: "{amp:amp}/about-us",
                    defaults: new { Controller = "AboutUs", Action = "Index" }
                );

                routes.MapRoute(
                    name: "About",
                    template: "about-us",
                    defaults: new { Controller = "AboutUs", Action = "Index" }
                );

                routes.MapRoute(
                    name: "HomeWithLanguageAmp",
                    template: "{amp:amp}/{language:language}",
                    defaults: new { Controller = "Home", Action = "Index" }
                );

                routes.MapRoute(
                    name: "HomeWithLanguage",
                    template: "{language:language}",
                    defaults: new { Controller = "Home", Action = "Index" }
                );

                routes.MapRoute(
                   name: "HomeWithAmp",
                   template: "{amp:amp}",
                   defaults: new { Controller = "Home", Action = "Index" }
                );
                routes.MapRoute(
                    name: "Home",
                    template: "",
                    defaults: new { Controller = "Home", Action = "Index" }
                );



                //routes.MapRoute(
                //  name: "home",
                //  template: "",
                //  defaults: new { controller = "Home", Action = "Index" }
                //);

                //routes.MapRoute(
                //     name: "about",
                //     template: "about",
                //     defaults: new { controller = "About", Action = "Index" }
                //);

                //routes.MapRoute(
                //    name: "category",
                //    template: "{category}",
                //    defaults: new { controller = "About", Action = "Index", category = "category1" }
                //);
            });
        }
    }

    public class FeatureLocationExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            // Don't need anything here, but required by the interface
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {

            // {0} - Action Name
            // {1} - Controller Name
            // {2} - Area Name            
            return new string[] {
                "/Web/{1}/{0}.cshtml",
                "/Web/Shared/{0}.cshtml"
            };

            // 我们废除 area 了
            //return new string[] {
            //    "/{2}/{1}/{0}.cshtml",
            //    "/{2}/Shared/{0}.cshtml"
            //};
        }

    }
}
