using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace iPractice
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            
            routes.LowercaseUrls = true;
            //routes.AppendTrailingSlash=true;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes(); 

            #region WORK SHEET ROUTES

            routes.MapRoute(
            name: "WorkSheetHome",
            url: "WorkSheets",
            defaults: new { controller = "Sheet", action = "Index" });

            routes.MapRoute(
            name: "WorkSheet",
            url: "WorkSheets/{id}",
            defaults: new { controller = "Sheet", action = "WorkSheets" });

            // worksheet/subtraction/25
            routes.MapRoute(
           "WorkSheetDetailOld",
           "WorkSheets/{topicid}/{sheetNum}", new { controller = "Sheet", action = "DetailsOld" }, new { sheetNum = @"\d+" });

            routes.MapRoute(
            name: "WorkSheetDetails",
            url: "WorkSheets/{id}/{subtopic}/{sheetNum}",
            defaults: new { controller = "Sheet", action = "Detail", sheetNum = UrlParameter.Optional });

            //routes.MapRoute(
            //name: "WorkSheetDetails",
            //url: "WorkSheets/{id}/{subtopic}/{sheetNum}",
            //defaults: new { controller = "Sheet", action = "Detail", sheetNum = UrlParameter.Optional });

            //routes.MapRoute(
            //"WorkSheetDetail",
            //"WorkSheets/{topicid}/{id}", new { controller = "Sheet", action = "details" }, new { id = @"\d+" });  

            //routes.MapRoute(
            //name: "PrintWorksheetHelp",
            //url: "WorkSheets/Print-Worksheet-Help",
            //defaults: new { controller = "Sheet", action = "WorksheetPrint" });

            //routes.MapRoute(
            //name: "SubmitWorksheetHelp",
            //url: "WorkSheets/Submit-Worksheet-Help",
            //defaults: new { controller = "Sheet", action = "WorksheetOnlineSubmit" });

            #endregion

            #region QUESTION PAGE ROUTES
            ////routes added for math-problem//

            routes.MapRoute(
            name: "math-problem",
            url: "math-problem",
            defaults: new { controller = "Question", action = "MathProblem"});

           routes.MapRoute(
           name: "math-problem_To_Topic",
           url: "math-problem/{topic}",
           defaults: new { controller = "Question", action = "Details", topic=UrlParameter.Optional });

            /////////
            //routes.MapRoute(
            //name: "GetQuestion",
            //url: "math-problem/{Query}/{id}",
            //defaults: new { controller = "Question", action = "Index", id = UrlParameter.Optional });

            //routes.MapRoute(
            //name: "LoadQuestion",
            //url: "math-problem/{Query}/{id}",
            //defaults: new { controller = "Question", action = "LoadQuestion", id = UrlParameter.Optional });

            //routes.MapRoute(
            //name: "SubmitQuestion",
            //url: "Question/SubmitQuestionAnswer/{id}",
            //defaults: new { controller = "Question", action = "SubmitQuestionAnswer", id = UrlParameter.Optional });

            routes.MapRoute(
            name: "math-problemHome",
            url: "math-practice",
            defaults: new { controller = "Topic", action = "Index" });

            routes.MapRoute(
              name: "MathPractice_To_Topic",
              url: "math-practice/{id}",
              defaults: new { controller = "Topic", action = "Details", id = UrlParameter.Optional });


            routes.MapRoute(
            name: "MathProblem_Redirect_To_Topic",
            url: "math-problem/{id}",
            defaults: new { controller = "Topic", action = "Details", id = UrlParameter.Optional });

            routes.MapRoute(
              name: "question",
              url: "math-problem/{Query}/{id}",
              defaults: new { controller = "Question", action = "Index", id = UrlParameter.Optional });


            #endregion

            #region GRADE PAGE ROUTES

            //routes.MapRoute(
            //  name: "GradeHome",
            //  url: "Grade",
            //  defaults: new { controller = "Grade", action = "Index" });

            //routes.MapRoute(
            //  name: "Grade",
            //  url: "Grade/Grade-{id}-skills",
            //  defaults: new { controller = "Grade", action = "Details", id = UrlParameter.Optional });
            
            //routes.MapRoute(
            // name: "Grade_Question",
            // url: "{Q}-Grade/{Query}/{id}",
            //defaults: new { controller = "Question", action = "Index", id = UrlParameter.Optional });

            #endregion

            #region STATIC PAGES, LOGIN, SIGN UP

            //routes.MapRoute(
            //name: "Login",
            //url: "Membership/SignIn",
            //defaults: new { controller = "Membership", action = "SignIn" });
            
            routes.MapRoute(
            name: "Register",
            url: "Membership/Register",
            defaults: new { controller = "Membership", action = "Register", id = UrlParameter.Optional});


            routes.MapRoute(
            name: "TermOfUse",
            url: "Home/Legal",
            defaults: new { controller = "Home", action = "Legal" });

            #endregion

            #region REPORTS

            //TempMethods(routes);

            routes.MapRoute(
            name: "Reports",
            url: "Reports",
            defaults: new { controller = "Reports", action = "Index" });

            #endregion

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

        }

       private static void IgnoreUrl(RouteCollection routes)
       {
		   #region Ignored Commented Routes
            //routes.IgnoreRoute("{resource}.html/{*pathInfo}");
            //routes.IgnoreRoute("robots.txt");
            //routes.IgnoreRoute("sitemap");
            //routes.IgnoreRoute("sitemap.gz");
            //routes.IgnoreRoute("sitemap.xml");
            //routes.IgnoreRoute("sitemap.xml.gz");
            //routes.IgnoreRoute("google_sitemap.xml");
            //routes.IgnoreRoute("google_sitemap.xml.gz");
            //routes.IgnoreRoute("favicon.ico");
			
			//routes.IgnoreRoute("{Content}/{*pathInfo}");
            //routes.IgnoreRoute("{blog}/{*pathInfo}");

            #endregion
        }

       private static void TempMethods(RouteCollection routes)
        {
            // todo - Needed To Remove Later
            routes.MapRoute(
            name: "HeroOfTheDay",
            url: "Reports/HeroesOfTheDay",
            defaults: new { controller = "Reports", action = "StudentsOfTheDay" });

            // todo - Needed To Remove Later
            routes.MapRoute(
           name: "HeroOfTheWeek",
           url: "Reports/HeroesOfTheWeek",
           defaults: new { controller = "Reports", action = "StudentsOfTheWeek" });

            // todo - Needed To Remove Later
            routes.MapRoute(
            name: "HeroOfTheMonth",
            url: "Reports/HeroesOfTheMonth",
            defaults: new { controller = "Reports", action = "StudentsOfTheMonth" });
        }
    }

    public class CustomRouteHandler : IRouteHandler
    {
        public System.Web.IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            requestContext.HttpContext.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.ReadOnly);
            return new MvcHandler(requestContext);
        }
    }
}