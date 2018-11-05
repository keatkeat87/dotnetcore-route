using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;


namespace Project.Web.Home
{
    public class HomeController : Controller
    {
        #region DI

        //private readonly IStringLocalizer<HomeController> Localizer;
        //private readonly IHtmlLocalizer<HomeController> HtmlLocalizer;
        public readonly IHttpContextAccessor HttpContextAccessor;

        public HomeController(
              //IStringLocalizer<HomeController> localizer,
              IHttpContextAccessor httpContextAccessor,
              IHtmlLocalizer<HomeController> HomeLocalizer
        )
        {
            //Localizer = localizer;
            HttpContextAccessor = httpContextAccessor;
            var ggc = HomeLocalizer["<i>Hello</i> <div>{0}!</div>", "<div>super</div>"];
        }

        #endregion
         
        public IActionResult Index()
        {
            var requestCulture = HttpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            //var x = Localizer["Call Us On"];
            //var xy = Localizer["Passengers"];

            //var mm = Localizer["<i>Hello</i> <b>{0}!</b>", "super"];
            //var mm2 = HtmlLocalizer["<i>Hello</i> <b>{0}!</b>", "super"];


            //var url = Url.RouteUrl(new UrlRouteContext
            //{
            //    RouteName = "about",
            //    Values = new
            //    {
            //        language = "cn",
            //        name = "keatkeat",
            //        age = 11,
            //        currentYear = true
            //    }
            //});

            //var url2 = Url.RouteUrl(new UrlRouteContext
            //{
            //    RouteName = "about",
            //    Values = new
            //    {
            //        language = "cn",
            //        age = 11,
            //        currentYear = true
            //    }
            //});

            return View();
        }

        
    }
}
