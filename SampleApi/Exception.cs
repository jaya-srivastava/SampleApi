using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Elmah;
using Elmah.Mvc;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using IPracticeApi.Controllers;
using System.Web.Http.Filters;
using System.Configuration;

namespace IPracticeApi
{
    public class LogRequestAndResponseHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //if Web.config seeting True  - Log it else don't log it.

            //logging request body
            string requestBody = await request.Content.ReadAsStringAsync();
            //System.Diagnostics.Debug.WriteLine(requestBody);
            //Trace.WriteLine(requestBody);

            //let other handlers process the request
            return await base.SendAsync(request, cancellationToken)
                .ContinueWith(task =>
                {
                    //once response is ready, log it
                    var responseBody = task.Result.Content.ReadAsStringAsync().Result;

                    if (task.Result.IsSuccessStatusCode == false || ConfigurationManager.AppSettings["TraceLogging"]=="true")
                    {
                        Trace.WriteLine(DateTime.Now.ToUniversalTime());
                        Trace.WriteLine(request.RequestUri);
                        Trace.WriteLine(request.Content);
                        Trace.WriteLine(responseBody);
                        Trace.WriteLine("------------------------------------------------------");
                    }
                    return task.Result;
                });
        }
    }
    public class TraceSourceExceptionLogger : ExceptionLogger
    {
        private readonly TraceSource _traceSource;

        public TraceSourceExceptionLogger(TraceSource traceSource)
        {
            _traceSource = traceSource;
        }

        public override void Log(ExceptionLoggerContext context)
        {
            _traceSource.TraceEvent(TraceEventType.Error, 1,
                "Unhandled exception processing {0} for {1}: {2}",
                context.Request.Method,
                context.Request.RequestUri,
                context.Exception);
        }
    }
    //public class ExceptionLogger : IExceptionLogger
    //{
    //    public virtual Task LogAsync(ExceptionLoggerContext context,
    //                                 CancellationToken cancellationToken)
    //    {
    //        if (!ShouldLog(context))
    //        {
    //            return Task.FromResult(0);
    //        }

    //        return LogAsyncCore(context, cancellationToken);
    //    }

    //    public virtual Task LogAsyncCore(ExceptionLoggerContext context,
    //                                     CancellationToken cancellationToken)
    //    {
    //        LogCore(context);
    //        return Task.FromResult(0);
    //    }

    //    public virtual void LogCore(ExceptionLoggerContext context)
    //    {
    //    }

    //    public virtual bool ShouldLog(ExceptionLoggerContext context)
    //    {
    //        IDictionary exceptionData = context.ExceptionContext.Exception.Data;

    //        if (!exceptionData.Contains("MS_LoggedBy"))
    //        {
    //            exceptionData.Add("MS_LoggedBy", new List<object>());
    //        }

    //        ICollection<object> loggedBy = ((ICollection<object>)exceptionData["MS_LoggedBy"]);

    //        if (!loggedBy.Contains(this))
    //        {
    //            loggedBy.Add(this);
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //}

    //class TraceExceptionLogger : ExceptionLogger
    //{
    //    public override void LogCore(ExceptionLoggerContext context)
    //    {
    //        Console.WriteLine(context.ExceptionContext.Exception.ToString());
    //        Trace.TraceError(context.ExceptionContext.Exception.ToString());
    //    }
    //}


     class TextPlainErrorResult : IHttpActionResult
    {
        public HttpRequestMessage Request { get; set; }

        public string Content { get; set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response =
                             new HttpResponseMessage(HttpStatusCode.InternalServerError);
            response.Content = new StringContent(Content);
            response.RequestMessage = Request;
            return Task.FromResult(response);
        }
    }
    
     class OopsExceptionHandler : ExceptionHandler
     {
         public override void HandleCore(ExceptionHandlerContext context)
         {
             context.Result = new TextPlainErrorResult
             {
                 Request = context.ExceptionContext.Request,
                 Content = "Oops! Sorry! Something went wrong." +
                           "Please contact support@iPracticemath.com so we can try to fix it."
             };
         }         
     }

    public class ExceptionHandler : IExceptionHandler
    {
        public virtual Task HandleAsync(ExceptionHandlerContext context,
                                        CancellationToken cancellationToken)
        {
            //if (!ShouldHandle(context))
            //{
            //    return Task.FromResult(0);
            //}

            return HandleAsyncCore(context, cancellationToken);
        }

        public virtual Task HandleAsyncCore(ExceptionHandlerContext context,
                                           CancellationToken cancellationToken)
        {
            HandleCore(context);
            return Task.FromResult(0);
        }

        public virtual void HandleCore(ExceptionHandlerContext context)
        {
        }

        //public virtual bool ShouldHandle(ExceptionHandlerContext context)
        //{
        //    return context.ExceptionContext.IsOutermostCatchBlock;
        //}
    } 

    public class ElmahExceptionLogger : ExceptionLogger
    {
        private const string HttpContextBaseKey = "MS_HttpContext";
        public override void Log(ExceptionLoggerContext context)
        {
            // Retrieve the current HttpContext instance for this request.
            HttpContext httpContext = GetHttpContext(context.Request);

            //if (httpContext == null) {  return; }

            // Wrap the exception in an HttpUnhandledException so that ELMAH can capture the original error page.
            Exception exceptionToRaise = new HttpUnhandledException(message: null, innerException: context.Exception);

            // Send the exception to ELMAH (for logging, mailing, filtering, etc.).
            ErrorHelper.LogErrorManually(exceptionToRaise, httpContext);
        }

        private static HttpContext GetHttpContext(HttpRequestMessage request)
        {
            HttpContextBase contextBase = GetHttpContextBase(request);
            if (contextBase == null)  {return null;}
            return ToHttpContext(contextBase);
        }
        private static HttpContextBase GetHttpContextBase(HttpRequestMessage request)
        {
            if (request == null) {return null;}
            object value;
            if (!request.Properties.TryGetValue(HttpContextBaseKey, out value))  { return null;}

            return value as HttpContextBase;
        }
        private static HttpContext ToHttpContext(HttpContextBase contextBase)
        {
            return contextBase.ApplicationInstance.Context;
        }
    }
    public static class ErrorHelper
    {
        /// <summary>
        /// Manually log an exception to Elmah.  This is very useful for the agents that try/catch all the errors.
        /// 
        /// In order for this to work elmah must be setup in the web.config/app.config file
        /// </summary>
        /// <param name="ex"></param>
        public static void LogErrorManually(Exception ex)
        {
            
            if (ex is HttpResponseException) ex = new Exception(System.Environment.NewLine + ((HttpResponseException)ex).Response.ToString());
            if (HttpContext.Current != null)//website is logging the error
            {
                var elmahCon = Elmah.ErrorSignal.FromCurrentContext();
                var type=ex.GetType();
                //if(ex.GetType() == HttpResponseException)
                elmahCon.Raise(ex);
            }
            else//non website, probably an agent
            {
                var elmahCon = Elmah.ErrorLog.GetDefault(null);
                elmahCon.Log(new Elmah.Error(ex));
            }
        }

        public static void LogErrorManually(Exception ex, HttpContext context)
        {
            if (HttpContext.Current != null)//website is logging the error
            {
                var elmahCon = Elmah.ErrorSignal.FromContext(context);
                elmahCon.Raise(ex,context);
            }
            else//non website, probably an agent
            {
                var elmahCon = Elmah.ErrorLog.GetDefault(null);
                elmahCon.Log(new Elmah.Error(ex));
            }
        }
    }


    //This is registered in WebApi.config and on Exception, we call ErrorHelper.LogErrorManually to log the execption
    //in ELMAH
    public class HttpNotFoundAwareDefaultHttpControllerSelector : DefaultHttpControllerSelector
    {
        public HttpNotFoundAwareDefaultHttpControllerSelector(HttpConfiguration configuration)
            : base(configuration)
        {
        }
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            HttpControllerDescriptor decriptor = null;
            try
            {
                decriptor = base.SelectController(request);
            }
            catch (HttpResponseException ex)
            {
                ErrorHelper.LogErrorManually(ex);
                var code = ex.Response.StatusCode;
                if (code != HttpStatusCode.NotFound)
                    throw;
                var routeValues = request.GetRouteData().Values;
                routeValues["controller"] = "Error";
                routeValues["action"] = "Error404";
                decriptor = base.SelectController(request);
            }
            return decriptor;
        }
    }

    //This is registered in WebApi.config and on Exception, we call ErrorHelper.LogErrorManually to log the execption
    //in ELMAH
    public class HttpNotFoundAwareControllerActionSelector : ApiControllerActionSelector
    {
        public HttpNotFoundAwareControllerActionSelector()
        {
        }

        public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
        {
            HttpActionDescriptor decriptor = null;
            try
            {
                decriptor = base.SelectAction(controllerContext);
            }
            catch (HttpResponseException ex)
            {
                ErrorHelper.LogErrorManually(ex);
                var code = ex.Response.StatusCode;
                if (code != HttpStatusCode.NotFound && code != HttpStatusCode.MethodNotAllowed)
                    throw;
                var routeData = controllerContext.RouteData;
                routeData.Values["action"] = "Error404";
                IHttpController httpController = new ErrorController();
                controllerContext.Controller = httpController;
                controllerContext.ControllerDescriptor = new HttpControllerDescriptor(controllerContext.Configuration, "Error", httpController.GetType());
                decriptor = base.SelectAction(controllerContext);
            }
            return decriptor;
        }
    }

   
}