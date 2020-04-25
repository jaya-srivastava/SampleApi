using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPracticeApi.Controllers
{    
    public class ErrorController : ApiController
    {
        // GET: Home

        [HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions, AcceptVerbs("PATCH")]
        public HttpResponseMessage Error404()
        {
            var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
            responseMessage.Content = new StringContent("The requested resource is not found");
            return responseMessage;
        }
    }

   
}