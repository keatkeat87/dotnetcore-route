using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;

namespace Project.Web.Home
{
    //[Route("contact")]
    public class ContactController : Controller
    {
        #region DI
        


        public ContactController(
             
        )
        {
           
        }
        #endregion
         
        //[Route("{contactPath=what}", Name = "contact")]
        public IActionResult Index([FromRoute] string contactPath)
        {
             
            return View();
        }

        
    }
}
