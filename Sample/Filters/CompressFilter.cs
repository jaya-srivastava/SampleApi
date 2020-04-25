using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iPractice.Filters
{
    //nother important thing is compression. Now a days, all modern browsers accept compressed contents 
    //and it saves huge bandwidth. You can apply the following action filter to compress your response in your  ASP.NET MVC application:

public class CompressOutputFilter : ActionFilterAttribute
{
    //public override void OnActionExecuting(ActionExecutingContext filterContext)
    public override void OnResultExecuting(ResultExecutingContext filterContext)
    {
        var result = filterContext.Result;
        if (!(result is ViewResult))
            return;

        HttpRequestBase request = filterContext.HttpContext.Request;
        string acceptEncoding = request.Headers["Accept-Encoding"];
        if (string.IsNullOrEmpty(acceptEncoding))
            return;

        acceptEncoding = acceptEncoding.ToUpperInvariant();

        HttpResponseBase response = filterContext.HttpContext.Response;
        if (acceptEncoding.Contains("GZIP"))
        {
            // we want to use gzip 1st
            response.AppendHeader("Content-encoding", "gzip");
            //Add DeflateStream to the pipeline in order to compress response on the fly 
            response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
        }
        else if (acceptEncoding.Contains("DEFLATE"))
        {
            //If client accepts deflate, we'll always return compressed content 
            response.AppendHeader("Content-encoding", "deflate");
            //Add DeflateStream to the pipeline in order to compress response on the fly 
            response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
        }
        else if (acceptEncoding.Contains("*"))
        {
            response.AppendHeader("Content-encoding", "gzip");
            //Add DeflateStream to the pipeline in order to compress response on the fly 
            response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
        }
    }
}



}