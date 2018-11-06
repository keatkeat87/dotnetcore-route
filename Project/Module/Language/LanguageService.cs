using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Project.Module.Amp;
using Project.Module.Stooges;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Module.Language
{
    public class LanguageService
    {
        public readonly LanguageOptions LanguageOptions;
        public readonly IHttpContextAccessor HttpContextAccessor;

        public LanguageService
        (
            IOptionsSnapshot<LanguageOptions> languageOptionsAccessor,
            IHttpContextAccessor httpContextAccessor
        )
        {
            LanguageOptions = languageOptionsAccessor.Value;
            HttpContextAccessor = httpContextAccessor;
        }

        public List<Language> SupportedLanguages { get { return LanguageOptions.SupportedLanguages; } }
        public Language DefaultLanguage { get { return LanguageOptions.DefaultLanguage; } }
        public Language CurrentLanguage
        {
            get
            {
                var requestCulture = HttpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>(); 
                return SupportedLanguages.Single(s => s.ISOCode == requestCulture.RequestCulture.UICulture.Name); 
            }
        }

        public string GetValueFromResource(object resource, string key)
        {
            key = key.Substring(0, key.Length - 2);
            var requestCulture = HttpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            var currentLanguage = LanguageOptions.SupportedLanguages.Single(s => s.ISOCode == requestCulture.RequestCulture.UICulture.Name);
            // c# property can't have - symbol. so like zh-Hans will become zhHans in c#. 
            // the script Hans 开头一定是大写的，所以 remove - 让它黏在一起就可以了 
            // refer : https://en.wikipedia.org/wiki/ISO_15924
            key = key + currentLanguage.ISOCode.Replace("-", "");
            return resource.GetValueByKey<string>(key);
        }

        public class Alternative
        {
            public string Url { get; set; }
            public Language Language { get; set; }
        }

        public List<Alternative> GetAlternativesByRequestUrl(string requestUrl)
        {
            var alternatives = new List<Alternative>();
            var uriBuilder = new UriBuilder(requestUrl);
            var originalSegments = uriBuilder.Path.Split('/').ToList();
            // note : segments[0] always is empty string so actually we take segments[1] 
            var firstSegment = originalSegments.ElementAt(1);
            var amp = firstSegment == RouteAmpConstraint.ConstraintNameValue;
            var withoutAmpAndLanguageSegments = new List<string>();

            for (var i = 0; i < originalSegments.Count(); i++)
            {
                var value = originalSegments[i];
                if ((i == 0 && amp) || (i <= 1 && SupportedLanguages.Any(s => s.ISOCode == value)))
                {
                    //skip amp & language
                }
                else
                {
                    withoutAmpAndLanguageSegments.Add(value);
                }
            }

            SupportedLanguages.ForEach(s =>
            {
                var isDefaultLanguage = s.ISOCode == DefaultLanguage.ISOCode;
                var segments = new List<string>();
                if (amp) segments.Add(RouteAmpConstraint.ConstraintNameValue);
                if (!isDefaultLanguage) segments.Add(s.ISOCode);
                segments = segments.Concat(withoutAmpAndLanguageSegments).ToList();
                uriBuilder.Path = string.Join('/', segments);
                alternatives.Add(new Alternative
                {
                    Language = s,
                    Url = uriBuilder.ToString()
                });

            });

            //foreach (var language in SupportedLanguages)
            //{
            //    var uriBuilder = new UriBuilder(requestUrl);
            //    var segments = uriBuilder.Path.Split('/').ToList();  
            //    var firstSegment = segments.ElementAt(1);
            //    // note : segments[0] always is empty string so actually we take segments[1] 
            //    if (firstSegment == "" || SupportedLanguages.Select(l => l.ShortFriendlyName).Contains(firstSegment))
            //    {
            //        segments.RemoveAt(1);
            //    }
            //    if (language.ShortFriendlyName == DefaultLanguage.ShortFriendlyName)
            //    {
            //        // skip
            //    }
            //    else
            //    {
            //        segments.Insert(1, language.ShortFriendlyName); 
            //    }
            //    uriBuilder.Path = string.Join('/', segments);
            //    alternatives.Add(new Alternative {
            //       Url = uriBuilder.ToString(),
            //       Language = language
            //    });
            //}

            return alternatives;
        }



    }
}


//public string AddLanguageToUrl(string path)
//{
//    var requestCulture = HttpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
//    var currentLanguage = LanguageOptions.SupportedLanguages.Single(s => s.ISOCode == requestCulture.RequestCulture.UICulture.Name).ShortFriendlyName;

//    var segments = path.Split('/').ToList();
//    if (currentLanguage == LanguageOptions.DefaultLanguage.ShortFriendlyName)
//    {
//        return path;
//    }
//    else
//    {
//        if (segments[1] == "")
//        {
//            segments[1] = currentLanguage;
//        }
//        else
//        {
//            segments.Insert(1, currentLanguage);
//        }
//        return string.Join('/', segments);
//    }
//}


//public bool NeedCanonical(string requestUrl)
//{
//    var uriBuilder = new UriBuilder(requestUrl);
//    var segments = uriBuilder.Path.Split('/').ToList();
//    return segments.Count() > 1 && segments.ElementAt(1) == DefaultLanguage.ShortFriendlyName;
//}

//public string RemoveDefaultLanguageInUrl(string requestUrl)
//{
//    var uriBuilder = new UriBuilder(requestUrl);
//    var segments = uriBuilder.Path.Split('/').ToList();
//    if (segments.Count() > 1 && SupportedLanguages.Select(l => l.ShortFriendlyName).Contains(segments.ElementAt(1)))
//    {
//        segments.RemoveAt(1);
//    }
//    uriBuilder.Path = string.Join('/', segments);
//    return uriBuilder.ToString();
//}
