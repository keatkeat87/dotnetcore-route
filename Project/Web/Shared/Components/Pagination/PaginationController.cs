namespace Project.Web.ViewComponents.Pagination
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.WebUtilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Pagination : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(int totalRow, int rowPerPage)
        {
            var currentUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";
            var uri = new Uri(currentUrl);
            var baseUri = uri.GetComponents(UriComponents.Scheme | UriComponents.Host | UriComponents.Port | UriComponents.Path, UriFormat.UriEscaped);
            var query = QueryHelpers.ParseQuery(uri.Query);
            var items = query.SelectMany(x => x.Value, (col, value) => new KeyValuePair<string, string>(col.Key, value)).ToList();
            var pageItem = items.FirstOrDefault(i => i.Key == "page");
            var page = (pageItem.Key == null) ? 1 : Convert.ToInt32(pageItem.Value); 

            var vm = new ViewModel
            {
                page = page,
                rowPerPage = rowPerPage,
                totalRow = totalRow,
                currentUrl = currentUrl
            };
            return Task.FromResult<IViewComponentResult>(View(vm));
        }
    }
}




