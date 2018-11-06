namespace Project.Web.ViewComponents.Breadcrumb
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class Breadcrumb : ViewComponent
    {

        public Task<IViewComponentResult> InvokeAsync(string secondPathName, string secondPath = null, string thirdPathName = null)
        {
            var vm = new BreadcrumbViewModel
            {
                secondPathName = secondPathName,
                secondPath = secondPath,
                thirdPathName = thirdPathName
            }; 
            return Task.FromResult<IViewComponentResult>(View(vm));
        }
    }
}
