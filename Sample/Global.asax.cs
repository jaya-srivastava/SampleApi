using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
//using iPractice.Service;
//using StackExchange.Profiling;
using System.Configuration;
using Sample.Core;
using Sample.Cache;
using System.Web.Helpers;
using System.Net;

namespace iPractice
{
    public class MvcApplication : System.Web.HttpApplication
    {
       
        private void ViewEngineInitialization()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine
            {
                ViewLocationFormats = new string[]
					 {
						"~/Views/{1}/{0}.cshtml",
                        "~/Views/Learn/BasicMath/{0}.cshtml",
						"~/Views/Learn/ConsumerMath/{0}.cshtml",
						"~/Views/Learn/Algebra/{0}.cshtml",
                        "~/Views/Learn/Statistics/{0}.cshtml",
                        "~/Views/Learn/Calculus/{0}.cshtml",
                        "~/Views/Learn/Decimal/{0}.cshtml",
                        "~/Views/Learn/Fraction/{0}.cshtml",                        
                        "~/Views/Learn/Trigonometry/{0}.cshtml",
                        "~/Views/Learn/Measurement/{0}.cshtml",
                        "~/Views/Learn/Integer/{0}.cshtml",
                        "~/Views/Learn/Geometry/{0}.cshtml",
                        "~/Views/Learn/RealNumber/{0}.cshtml",
                        "~/Views/Learn/NumberSense/{0}.cshtml",
                        "~/Views/Shared/{0}.cshtml",
					 },
            });

        }
      
        protected void Application_Start()
        {

            //MiniProfilerEF.InitializeEF42();
            //SessionStateutility class http://msdn.microsoft.com/en-us/library/system.web.sessionstate.sessionstateutility.aspx
            //No Intialization of Db as not Creating from Scratch 
            ////  Database.SetInitializer<PracticeContext>(null);
            //TempDataProvider all Set web.config/system.web/sessionState:mode="Off".
            //ControllerBuilder.Current.SetControllerFactory( new DictionaryTempDataControllerFactory() );
            //AppDomain.CurrentDomain.SetData("SQLServerCompactEditionUnderWebHosting", true);
            //RouteTable.Routes.IgnoreRoute("elmah.axd");

            MvcHandler.DisableMvcResponseHeader = true;
            // AreaRegistration.RegisterAllAreas();
            // WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            string Isbundle = System.Configuration.ConfigurationManager.AppSettings["IsBundle"].ToString();
            if (Isbundle == "true")
            {
                BundleTable.EnableOptimizations = true;
            }
            else
            {
                BundleTable.EnableOptimizations = false;
            }
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            AntiForgeryConfig.SuppressIdentityHeuristicChecks = true;

            ViewEngineInitialization();            
     

            // InitializeMemCacheClient();
            //AutoMapperConfigurator.Configure();
            //for pPal
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        protected void application_beginrequest()
        {
            //if (Request.IsLocal || Request.QueryString.AllKeys.Contains("showprofile"))
            //MiniProfiler.Start();
        }

        protected void application_endrequest()
        {
            //MiniProfiler.Stop();
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            Session["init"] = 0;
        }

        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Remove("Server");
            Response.Headers.Remove("X-AspNet-Version");
            Response.Headers.Remove("X-AspNetMvc-Version");
        }

        public override string GetOutputCacheProviderName(HttpContext context)
        {
			//to disable outputcache for LearnPage - just make sure that there is no method with DonutOutputcache provider
            if (ConfigHelper.IsLearnPageCacheEnabled && context.Request.RawUrl.ToLower().Contains("/learn/"))
            {
                if (ConfigHelper.IsMembasedOutputCacheEnabled)
                    return "MemcacheOutputCacheProvider"; //exactly same name as in web.config
                else
                {
                    //return "MemcacheOutputCacheProvider";
                    return "AspNetInternalProvider";
                }
            }

            string providerName = base.GetOutputCacheProviderName(context);
            return providerName;
        }

        private static void DisableInDebugMode()
        {
            #if DEBUG
            // Don't minify in debug mode
            foreach (var bundle in BundleTable.Bundles)
            {
                bundle.Transforms.Clear();
            }
            #endif
        }

    }
}
