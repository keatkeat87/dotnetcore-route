namespace Project.Web.ViewComponents.Test
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class Test : ViewComponent
    {
        //private ApplicationDbContext db { get; set; }

        public Test(
            //ApplicationDbContext db
        )
        {
            //this.db = db;
        }

        public Task<IViewComponentResult> InvokeAsync(string value)
        {
            var vm = new ViewModel
            {
                name = value
            };
            return Task.FromResult<IViewComponentResult>(View(vm));
        }
    }
}
