using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Module.Language
{
    public class RouteLanguageConstraint : IRouteConstraint
    {
        public static readonly string segmentName = "language";

        public bool Match(HttpContext httpContext,
            IRouter route,
            string routeKey,
            RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            var languageOptionsAccessor = httpContext.RequestServices.GetService(typeof(IOptionsSnapshot<LanguageOptions>)) as IOptionsSnapshot<LanguageOptions>;
            var languageOptions = languageOptionsAccessor.Value;
            //validate input params  
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));
            if (route == null) throw new ArgumentNullException(nameof(route));
            if (routeKey == null) throw new ArgumentNullException(nameof(routeKey));
            if (values == null) throw new ArgumentNullException(nameof(values));

            if (values.TryGetValue(routeKey, out var routeValue))
            {
                string language = routeValue.ToString();
                return languageOptions.SupportedLanguages
                    .Where(s => s.ISOCode != languageOptions.DefaultLanguage.ISOCode)
                    .Select(l => l.ISOCode.ToLower()).Contains(language.ToLower());
            }

            return false;
        }
    }
}
