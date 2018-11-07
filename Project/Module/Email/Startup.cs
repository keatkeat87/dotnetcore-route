using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Module.Email
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEmail(
            this IServiceCollection services
        )
        {
            services.AddSingleton<ICompositeViewEngine, CompositeViewEngine>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<EmailService, EmailService>();
            return services;
        }
    }   
}
