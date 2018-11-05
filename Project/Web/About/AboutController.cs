using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;

namespace Project.Web.Home
{
    public class AboutController : Controller
    {
        #region DI
        


        public AboutController(
             
        )
        {
           
        }
        #endregion
         
        public IActionResult Index([FromQuery] string value)
        {
             
            return View();
        }

        
    }
}
