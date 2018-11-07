using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Project.Module.Email
{  
    public class EmailService 
    {
        // Di
        #region
        private readonly EmailOptions EmailOptions;
        private readonly ICompositeViewEngine CompositeViewEngine;
        private readonly IServiceProvider ServiceProvider;
        private readonly ITempDataProvider TempDataProvider;

        public EmailService(
          IOptionsSnapshot<EmailOptions> emailOptionsAccessor,
          ICompositeViewEngine compositeViewEngine,
          IServiceProvider serviceProvider,
          ITempDataProvider tempDataProvider
        )
        {
            EmailOptions = emailOptionsAccessor.Value;
            CompositeViewEngine = compositeViewEngine;
            ServiceProvider = serviceProvider;
            TempDataProvider = tempDataProvider;
        }
        #endregion

        private async Task<string> GenerateBodyFromTemplateAsync(string templatePath, object model, string subject)
        {
            string body;
            using (StringWriter sw = new StringWriter())
            {
                // 这里渲染模板是不包含任何 http 请求的东西的, 所以模板里请不要使用 http 的东西哦 
                var httpContext = new DefaultHttpContext();
                httpContext.RequestServices = ServiceProvider;
                var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
                var viewData = new ViewDataDictionary(metadataProvider: new EmptyModelMetadataProvider(), modelState: new ModelStateDictionary());
                viewData.Model = model;
                var data = new TempDataDictionary(actionContext.HttpContext, TempDataProvider);
                var viewResult = CompositeViewEngine.GetView(null, templatePath, false);
                var viewContext = new ViewContext(actionContext, viewResult.View, viewData, data, sw, new HtmlHelperOptions());
                viewContext.ViewBag.Layout = EmailOptions.LayoutPath;
                viewContext.ViewBag.Title = subject;
                await viewResult.View.RenderAsync(viewContext);
                body = sw.GetStringBuilder().ToString();
            }
            return body;
        }

        public async Task SendAsync(
            string recipients,
            string subject,
            string body = null,
            string templatePath = null,
            object model = null,
            List<Attachment> attachs = null,
            string Bccs = null,
            string CCs = null,
            string replyTos = null,
            EmailOptions emailOptions = null            
        )
        {
            emailOptions = emailOptions ?? EmailOptions; // set default config
            var smtp = new SmtpClient
            {
                EnableSsl = emailOptions.EnableSsl,
                Port = emailOptions.Port,
                Host = emailOptions.Host,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(emailOptions.Username, emailOptions.Password)
            };

            MailMessage mail = new MailMessage
            {
                From = new MailAddress(emailOptions.From, emailOptions.DisplayName),
                Subject = subject,
                IsBodyHtml = true
            };

            mail.Body = body ?? await GenerateBodyFromTemplateAsync(templatePath, model, subject);
            recipients.Split(',').ToList().ForEach(recipient => mail.To.Add(recipient));
            if (Bccs != null) Bccs.Split(',').ToList().ForEach(Bcc => mail.Bcc.Add(Bcc));
            if (CCs != null) CCs.Split(',').ToList().ForEach(CC => mail.CC.Add(CC));
            if (replyTos != null) replyTos.Split(',').ToList().ForEach(replyTo => mail.ReplyToList.Add(replyTo));
            if (attachs != null) attachs.ForEach(attach => mail.Attachments.Add(attach));
            await smtp.SendMailAsync(mail);
        }
    } 
}
