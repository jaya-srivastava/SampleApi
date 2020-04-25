using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;

namespace System.Web.Mvc
{
    public static class UrlHelperExtensions
    {
        internal static Uri ActionFull(this UrlHelper urlHelper, string actionName)
        {
            return new Uri(HttpContext.Current.Request.Url, urlHelper.Action(actionName));
        }

        internal static Uri ActionFull(this UrlHelper urlHelper, string actionName, string controllerName)
        {
            return new Uri(HttpContext.Current.Request.Url, urlHelper.Action(actionName, controllerName));
        }

        /// <summary>
        /// Returns an absolute url for an action
        /// </summary>
        /// <param name="url">UrlHelper</param>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static string AbsoluteAction(this UrlHelper url, string action, object routeValues)
        {
            Uri requestUrl = url.RequestContext.HttpContext.Request.Url;

            string absoluteAction = string.Format("{0}://{1}{2}",
                                                  requestUrl.Scheme,
                                                  requestUrl.Authority,
                                                  url.Action(action, routeValues));

            return absoluteAction;
        }
        public static string AbsoluteAction(this UrlHelper url, string scheme, string action, object routeValues)
        {
            Uri requestUrl = url.RequestContext.HttpContext.Request.Url;

            string absoluteAction = string.Format("{0}://{1}{2}",
                                                  scheme,
                                                  requestUrl.Authority,
                                                  url.Action(action, routeValues));

            return absoluteAction;
        }

		public static MvcHtmlString ActionLinkBC(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
		{
			//string actionLink = htmlHelper.Action(linkText, actionName, controllerName).ToHtmlString();
			var actionlinkBC = String.Format("<div itemscope itemtype=\"http://data-vocabulary.org/Breadcrumb\"><a itemprop=\"url\" href=\"/{0}/{1}\">{2}</a>=></div>", controllerName, actionName, linkText);
			return MvcHtmlString.Create(actionlinkBC);
		}

		public static MvcHtmlString TextBC(this HtmlHelper htmlHelper, string text)
		{
			var actionlink = String.Format("<div itemscope itemtype=\"http://data-vocabulary.org/Breadcrumb\">{0}</div>", text);
			return MvcHtmlString.Create(actionlink);
		}

        public static MvcHtmlString IconActionLink(this HtmlHelper htmlHelper, string text, string url, string cssClass, string IconClass,string id)
        {
            var actionlink = String.Format("<a href='{0}' class='{1}'><i class='{2}' id='{3}'></i>{4}</a>",url,cssClass,IconClass,id,text); 
            return MvcHtmlString.Create(actionlink);
        }
        public static MvcHtmlString IconActionLink(this HtmlHelper htmlHelper, string text, string url, string cssClass, string IconClass)
        {
            var actionlink = String.Format("<a href='{0}' class='{1}'><i class='{2}'></i>{3}</a>", url, cssClass, IconClass, text);
            return MvcHtmlString.Create(actionlink);
        }

        public static MvcHtmlString IconActionLink(this HtmlHelper htmlHelper, string text, string url, string cssClass, string IconClass, string id, string htmlAttributes)
        {
            var actionlink = String.Format("<a href='{0}' {1} class='{2}' id='{3}'><i class='{4}'></i>{5}</a>", url,htmlAttributes ,cssClass,id, IconClass, text);
            return MvcHtmlString.Create(actionlink);
        }

        //***************microdata actionlink html Helper***************************************
        public static MvcHtmlString MicrodataActionLink(this HtmlHelper htmlHelper, string text, string url)
        {
            var actionlink = String.Format("<div class='displayinline' itemscope itemtype='http://data-vocabulary.org/Breadcrumb'>" +
                                            "<a href='{0}' itemprop='url'>" +
                                            "<span itemprop='title'>{1}</span> </a>" +
                                            "</div>", url, text);
            return MvcHtmlString.Create(actionlink);
        }

        public static MvcHtmlString MicrodataChildActionLink(this HtmlHelper htmlHelper, string text, string url)
        {
            var actionlink = String.Format("<div class='displayinline' itemprop='child' itemscope itemtype='http://data-vocabulary.org/Breadcrumb'>"+
                                            "<a href='{0}' itemprop='url'>"+
                                            "<span itemprop='title'>{1}</span> </a>"+
                                            "</div>", url, text);
            return MvcHtmlString.Create(actionlink);
        }

        //******************************End***********************************************
    }
}
