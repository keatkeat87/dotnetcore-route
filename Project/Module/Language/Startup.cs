using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Project.Module.Amp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Module.Language
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLanguage(
            this IServiceCollection services,
            Action<LanguageOptions> setupLanguageOptions
        )
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.Configure(setupLanguageOptions);
            services.AddScoped<LanguageService, LanguageService>();

            var serviceProvider = services.BuildServiceProvider();
            var languageOptions = serviceProvider.GetService<IOptionsSnapshot<LanguageOptions>>().Value;
            services.AddLocalization();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = languageOptions.SupportedLanguages.Select(s => new CultureInfo(s.ISOCode)).ToList();
                options.DefaultRequestCulture = new RequestCulture(culture: languageOptions.DefaultLanguage.ISOCode, uiCulture: languageOptions.DefaultLanguage.ISOCode);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
                {
                    var uriBuilder = new UriBuilder(context.Request.GetDisplayUrl());
                    var segments = uriBuilder.Path.Split('/').ToList();
                    var languageSegment = segments.ElementAt(1);
                    if (languageSegment == RouteAmpConstraint.ConstraintNameValue && segments.Count() >= 3) languageSegment = segments.ElementAt(2);
                    var language = languageOptions.SupportedLanguages.SingleOrDefault(s => s.ISOCode.ToLower() == languageSegment.ToLower()) ?? languageOptions.DefaultLanguage;
                    return Task.FromResult(new ProviderCultureResult(language.ISOCode));
                }));
            });

            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.SubFolder)
                .AddDataAnnotationsLocalization();

            services.Configure<RouteOptions>(routeOptions =>
            {
                routeOptions.ConstraintMap.Add(RouteLanguageConstraint.segmentName, typeof(RouteLanguageConstraint));
            });

            return services;
        }
    }

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseLanguage(
            this IApplicationBuilder appBuilder, 
            IServiceProvider serviceProvider
        )
        {
            appBuilder.UseRequestLocalization(serviceProvider.GetService<IOptions<RequestLocalizationOptions>>().Value); 
            return appBuilder;
        }
    }
}
