using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;

namespace Project.Web.Home
{
    public class AboutUsController : Controller
    {
        #region DI
        


        public AboutUsController(
             
        )
        {
           
        }
        #endregion
                
        public IActionResult Index([FromRoute] string language, [FromRoute] string amp)
        {
             
            return View();
        }

        
    }
}
