using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iPractice.Filters
{

    /// <summary>
    /// Caching plays a major role in developing highly scalable web applications. 
    /// We can cache any http get request in the user browser for a predefined time, 
    /// if the user request the same URL in that predefined time the response will be loaded from the browser cache instead of the server. 
    /// You can archive the same in ASP.NET MVC application with the following action filter:  
    /// [CacheFilter(Duration = 60)]
    /// http://weblogs.asp.net/rashid/archive/2008/03/28/asp-net-mvc-action-filter-caching-and-compression.aspx
    /// this will tell the BROWSER to cache item for so much duration so that broswer will cache the item for 60 seconds
    /// as Caching will become public 
    /// this seems to be good candidate for Learn Pages but what would be impact on TypeSetting from MathJax ??  hopefully should not be cached
    /// </summary>
    public class CacheFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Gets or sets the cache duration in seconds. The default is 10 seconds.
        /// </summary>
        /// <value>The cache duration in seconds.</value>
        public int Duration
        {
            get;
            set;
        }

        public CacheFilterAttribute()
        {
            Duration = 10;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (Duration <= 0) return;

            HttpCachePolicyBase cache = filterContext.HttpContext.Response.Cache;
            TimeSpan cacheDuration = TimeSpan.FromSeconds(Duration);

            cache.SetCacheability(HttpCacheability.Public);
            cache.SetExpires(DateTime.Now.Add(cacheDuration));
            cache.SetMaxAge(cacheDuration);
            cache.AppendCacheExtension("must-revalidate, proxy-revalidate");
        }
    }
}


