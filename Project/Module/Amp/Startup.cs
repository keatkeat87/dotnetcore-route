using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Module.Amp
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAmp(
            this IServiceCollection services
        )
        {
            services.Configure<RouteOptions>(routeOptions =>
            {
                routeOptions.ConstraintMap.Add(RouteAmpConstraint.ConstraintNameValue, typeof(RouteAmpConstraint));
            });
            
            return services;
        }
    }   
}
