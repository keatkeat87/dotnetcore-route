using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;

namespace Project.Web.Home
{
    public class CategoryController : Controller
    {
        #region DI
        


        public CategoryController(
             
        )
        {
           
        }
        #endregion
         
        public IActionResult Index([FromRoute] string category)
        {
             
            return View();
        }

        
    }
}
