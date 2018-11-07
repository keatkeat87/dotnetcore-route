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
using Project.Module.Email;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Web.Home
{
    public class HomeController : Controller
    {
        #region DI

        private readonly IHtmlLocalizer<HomeController> HtmlLocalizer;
        private readonly IStringLocalizer<HomeController> StringLocalizer;
        public readonly IHttpContextAccessor HttpContextAccessor;
        private readonly EmailService EmailService;


        public HomeController(
              //IStringLocalizer<HomeController> localizer,
              IHttpContextAccessor httpContextAccessor,
              IHtmlLocalizer<HomeController> htmlLocalizer,
              IStringLocalizer<HomeController> stringLocalizer,
              EmailService emailService
        )
        {
            //Localizer = localizer;
            HttpContextAccessor = httpContextAccessor;
            HtmlLocalizer = htmlLocalizer;
            StringLocalizer = stringLocalizer;
            EmailService = emailService;
        }

        #endregion
         
        public async Task<IActionResult> Index([FromRoute] string language, [FromRoute] string amp)
        {

            var xx = ViewBag.data;
            ViewBag.data = "controller value";

           // await EmailService.SendAsync(
           //   recipients: "hengkeat87@gmail.com",
           //   subject: "Best Way Website Enquiry",
           //   templatePath: "~/EmailTemplate/Contact/Index.cshtml",
           //   replyTos: "hengkeat87@gmail.com",
           //   model: new Email.Contact.ViewModel
           //   {
           //       email = "koocool@gmail.com",
           //       message = "msg",
           //       name = "keatkeat",
           //       subject = "test"
           //   }
           //   // attachs: model.fileFullPaths.Select(f => new Attachment(f)).ToList()
           //);


            //var x = Url.Action("Index", "AboutUsController", null, "https", "www.stooges.com.my");
            
            var resultA = HtmlLocalizer["test {0} with params", "dada"];
            var value = resultA.Value; // 注意 :这里很奇葩的哦， 是翻译成功了，但是 dada 没有被放入 {0} 里面, 这个放入的动作是留给 razor view 执行的. 
            value = HtmlLocalizer.GetString("test {0} with params", "dada"); //用 GetString 就可以完全成功，我是不知道为什么啦 


            var resultB = StringLocalizer["test {0} with params", "dada"];
            var resultBInEn = StringLocalizer.WithCulture(new CultureInfo("en"))["test {0} with params", "dada"]; // 可以动态选语言哦. 
                       

            var resultAA = HtmlLocalizer["<p>test html with {0}</p>", "<div>123</div>"];
            var resultBB = StringLocalizer["<p>test html with {0}</p>", "<div>123</div>"];
            
            var requestCulture = HttpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>(); // 获取请求的 locale
            

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

            if(string.IsNullOrEmpty(amp)){
                return View();
            }
            else{
                return View("~/Web/Home/Amp/Index.cshtml");
            }

        }

        
    }
}
