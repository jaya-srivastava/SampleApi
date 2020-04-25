using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

using System.Diagnostics;
using System.Configuration;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Dispatcher;
using System.Web.Http.Controllers;

namespace IPracticeApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Services.Replace(typeof(IHttpControllerSelector), new HttpNotFoundAwareDefaultHttpControllerSelector(config));
            config.Services.Replace(typeof(IHttpActionSelector), new HttpNotFoundAwareControllerActionSelector());
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );           
            
            //Enable cross origin requests            
            var apiUrls = ConfigurationManager.AppSettings["CorsAllowedUrl"] as string;//contains comma separated list
            var corsAttr = new EnableCorsAttribute(apiUrls, "*", "*");
            config.EnableCors(corsAttr);//config.EnableCors();

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));


            GlobalConfiguration.Configuration.Formatters.XmlFormatter.MediaTypeMappings.Add(new QueryStringMapping("xml", "true", "application/xml"));
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            #region Exception Handling and Logging

             // There can be multiple exception loggers. (By default, no exception loggers are registered.)
            config.Services.Add(typeof(IExceptionLogger), new ElmahExceptionLogger());
            
            //config.Services.Add(typeof(IExceptionLogger), new TraceExceptionLogger());
            config.Services.Add(typeof(IExceptionLogger), new TraceSourceExceptionLogger(new TraceSource("MyTraceSource", SourceLevels.All)));

            // There must be exactly ONE exception handler. (There is a default one that may be replaced.)
            // To make this sample easier to run in a browser, replace the default exception handler with one that sends
            // back text/plain content for all errors.
            //config.Services.Replace(typeof(IExceptionHandler), new GenericTextExceptionHandler());
            
            config.Services.Replace(typeof(IExceptionHandler), new OopsExceptionHandler());

            //one logger
            config.MessageHandlers.Add(new LogRequestAndResponseHandler());

            #endregion

        }
    }
}
