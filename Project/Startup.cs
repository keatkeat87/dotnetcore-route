using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new FeatureLocationExpander());
            });

            //services.Configure<RouteOptions>(options =>
            //{
            //    options.AppendTrailingSlash = false;
            //    options.LowercaseUrls = true;
            //});  

            services.AddLocalization();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo> { new CultureInfo("en"), new CultureInfo("zh-Hans") };

                options.DefaultRequestCulture = new RequestCulture(culture: "en", uiCulture: "en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
                {
                    return Task.FromResult(new ProviderCultureResult("zh-Hans"));
                    //var uriBuilder = new UriBuilder(context.Request.GetDisplayUrl());
                    //var firstSegment = uriBuilder.Path.Split('/').ToList().ElementAt(1);
                    //var language = languageOptions.SupportedLanguages.SingleOrDefault(s => s.ShortFriendlyName == firstSegment) ?? languageOptions.DefaultLanguage;
                    //return Task.FromResult(new ProviderCultureResult(language.ISOCode));
                }));
            });

            services.AddMvc()
              .AddViewLocalization(LanguageViewLocationExpanderFormat.SubFolder)
              .AddDataAnnotationsLocalization();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env,
            IServiceProvider serviceProvider
        )
        {
            app.UseRequestLocalization(serviceProvider.GetService<IOptions<RequestLocalizationOptions>>().Value);

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
            //app.UseCookiePolicy();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "home",
                  template: "",
                  defaults: new { controller = "Home", Action = "Index" }
                );

                routes.MapRoute(
                     name: "about",
                     template: "about",
                     defaults: new { controller = "About", Action = "Index" }
                );

                routes.MapRoute(
                 name: "category",
                 template: "{category}",
                 defaults: new { controller = "About", Action = "Index", category = "category1" }
               );
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
