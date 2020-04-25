using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iPractice.Helpers
{
    public static class CdnUtils
    {
        // <summary>
        /// This extension method is used to generate a URL path
        /// for the CDN depending on whether or not we are in release mode.
        /// </summary>
        /// <param name="helper">The MVC HTML helper that is being used.</param>
        /// <param name="contentPath">The path of the content. Normally starts with a ~</param>
        /// <returns>Returns a full URL based on whether or not in release mode</returns>
        /// Usage : <img src="@Html.CdnUrl("~/Content/Images/surfing-homepage.png" />
        /// add in AppSettings Section      //<!— The URL of the CDN ->
        //<add key="CDNUrl" value=" http://88600723r47.cf3.rackcdn.com"/>			
        //
        //public static string SiteUrl = ConfigurationManager.AppSettings["cdn1"].ToString().ToLowerInvariant();
        public static string CdnUrl(this HtmlHelper helper, string contentPath)
        {
            // Check if we are in release mode. If not, then simply return as normal
            #if (!DEBUG)								

            // The content path will often get passed in with a leading ‘~’ sign. We need to remove it // if we append the URL of the CDN to it.
                if (contentPath.StartsWith("~"))					
                {
                    contentPath = contentPath.Substring(1);
                }

                // Retrieve the URL of the CDN from the Web.config
                string appSetting = ConfigurationManager.AppSettings["CDNUrl"];	

                // Combine the two URLs and update the content
                Uri combinedUri = new Uri(new Uri(appSetting), contentPath);
            contentPath = combinedUri.ToString();					
            
            #endif

            // Create the correct URL
            var url = new UrlHelper(helper.ViewContext.RequestContext);

            // Return the new and updated content path
            return url.Content(contentPath);
        }
    }
}