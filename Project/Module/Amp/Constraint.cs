using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Module.Amp
{
    public class RouteAmpConstraint : IRouteConstraint
    {
        public static readonly string ConstraintNameValue = "amp";

        public bool Match(HttpContext httpContext,
            IRouter route,
            string routeKey,
            RouteValueDictionary values,
            RouteDirection routeDirection)
        {

            //validate input params  
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));
            if (route == null) throw new ArgumentNullException(nameof(route));
            if (routeKey == null) throw new ArgumentNullException(nameof(routeKey));
            if (values == null) throw new ArgumentNullException(nameof(values));

            if (values.TryGetValue(routeKey, out var routeValue))
            {
                string amp = routeValue.ToString().ToLower();
                return amp == ConstraintNameValue;
            }

            return false;
        }
    }
}
