using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Web.SessionState;
using iPractice.RestServices;
using iPractice.Helpers;


namespace iPractice.Controllers
{
    //[HandleErrorWithELMAHAttribute]
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class AppController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
            {
            if (!HttpContext.IsDebuggingEnabled)
            {
                ViewBag.LearnCdnURL = ConfigurationManager.AppSettings["learncdn"];
                ViewBag.cdn1URL = ConfigurationManager.AppSettings["cdn1"];
            }
            if (Request.IsAjaxRequest())
                ViewBag.CurrentURL = HttpContext.Request.UrlReferrer.LocalPath ?? "";
            else
                ViewBag.CurrentURL = HttpContext.Request.Url.ToString() ?? "";
        }

       

        protected ViewResult PageNotFound(string description=null, string returnurl=null)
        {
            Response.StatusCode = 404;
            if (!String.IsNullOrWhiteSpace(description)) ViewBag.Message = description;
            if (!String.IsNullOrWhiteSpace(returnurl)) ViewBag.returnUrl = returnurl.ToLower();
            return View("~/Views/Home/Error404.cshtml");
        }


        protected ActionResult PermanentPageRedirect(string newUrl, string returnurl = null)
        {
            Response.StatusCode = 301;
            // if (!String.IsNullOrWhiteSpace(description)) ViewBag.Message = description;
            if (!String.IsNullOrWhiteSpace(returnurl)) ViewBag.returnUrl = returnurl.ToLower();

            return RedirectToActionPermanent(newUrl);

        }

        //protected ViewResult UnAuthorized(string description = null, string returnurl = null)
        //{
        //    Response.StatusCode = 401;
        //    if (!String.IsNullOrWhiteSpace(description)) ViewBag.Message = description;
        //    if (!String.IsNullOrWhiteSpace(returnurl)) ViewBag.returnUrl = returnurl;

        //    return View("~/Views/Home/Error404.cshtml");
        //}


    }
}
