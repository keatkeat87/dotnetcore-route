using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Web.ViewComponents.Pagination
{
    public class ViewModel
    {
        public int page { get; set; }
        public int rowPerPage { get; set; }
        public int totalRow { get; set; }  
        public string currentUrl { get; set; }
        public int lastPage
        {         
            get
            {
                double result = (double)totalRow / (double)rowPerPage;
                return (int)Math.Ceiling(result);
            }
        }
        public List<int> range(int start,int count)
        {
            return Enumerable.Range(start, count).ToList();
        }

        public string getUrl(int nextPage)
        {         
            var uri = new Uri(currentUrl);
            var baseUri = uri.GetComponents(UriComponents.Scheme | UriComponents.Host | UriComponents.Port | UriComponents.Path, UriFormat.UriEscaped);
            var query = QueryHelpers.ParseQuery(uri.Query);
            var items = query.SelectMany(x => x.Value, (col, value) => new KeyValuePair<string, string>(col.Key, value)).ToList();
            items.RemoveAll(x => x.Key == "page"); 
            var qb = new QueryBuilder(items);
            qb.Add("page", nextPage.ToString());
            return baseUri + qb.ToQueryString();
        }
    }
}
