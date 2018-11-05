using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Project.Models;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string urlTitle)
        {
            var url = Url.RouteUrl(new UrlRouteContext {
                RouteName = "about",
                Values = new {
                    language = "cn",
                    name = "keatkeat",
                    age = 11,
                    currentYear = true
                }
            });

            var url2 = Url.RouteUrl(new UrlRouteContext
            {
                RouteName = "about",
                Values = new
                {
                    language = "cn",
                    age = 11,
                    currentYear = true
                }
            });
            return View();
        }

        public IActionResult About([FromRoute] string language, [FromQuery] int age, [FromQuery] bool currentYear)
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
