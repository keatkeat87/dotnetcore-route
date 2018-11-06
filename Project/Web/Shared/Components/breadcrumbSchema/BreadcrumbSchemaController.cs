namespace Project.Web.ViewComponents.BreadcrumbSchema
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class BreadcrumbSchema : ViewComponent
    {

        public Task<IViewComponentResult> InvokeAsync(string secondPathName, string secondPath, string thirdPathName = null, string thirdPath = null)
        {
            var vm = new BreadcrumbSchemaViewModel
            {
                secondPath = secondPath,
                secondPathName = secondPathName,
                thirdPath = thirdPath,
                thirdPathName = thirdPathName
            }; 
            return Task.FromResult<IViewComponentResult>(View(vm));
        }
    }
}
