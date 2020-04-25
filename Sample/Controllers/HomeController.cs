using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using iPractice.Helpers;
using iPractice.RestServices;
using Sample.Models.DTO;


namespace iPractice.Controllers
{
    public class HomeController : AppController
    {
        //private QuestionService _questionService = new QuestionService();
        #region FAQContactUsEtc
        //public async Task<ActionResult> Index()
        public ActionResult Index()
        {
            //int hash = HashString(Session.SessionID);
            //var lstQuestions =await _questionService.GetQuestionsforHomePage();
            return View();
        }       

        public ActionResult FAQs()
        {
            return View();
        }

        public ActionResult Legal()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }
      
        public ActionResult WhyUs()
        {
            return View();
        }

        public ActionResult WhyMathPractice()
        {
            return View();
        }      
        
        public ActionResult Sitemap()
        {
            return View();
        }

        #endregion

        #region ErrorPages

        public ContentResult angularerrors(string errorUrl, string errorMessage, string stackTrace, string cause)
        {
            String exceptionDetails = String.Format("Url : {0} \n ErrorMessage: {1} \n stackTrace: {2}, cause : {3}", errorUrl, errorMessage, stackTrace, cause);
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(exceptionDetails));
            return Content("from server " + errorMessage);
        }

        public ActionResult Error404(string returnUrl)
        {
            Response.StatusCode = 404;
            ViewBag.ErrorStatus = "404";
            ViewBag.ErrorMessage = "The Page cann't be found";
            Response.TrySkipIisCustomErrors = true;
            if (!string.IsNullOrWhiteSpace(returnUrl))
                ViewBag.returnUrl = returnUrl;
            return View();
        }

        public ActionResult Error500(string returnUrl)
        {
            Response.StatusCode = 500;
            ViewBag.ErrorStatus = "500";
            ViewBag.ErrorMessage = "Some error occured";
            Response.TrySkipIisCustomErrors = true;
            if (!string.IsNullOrWhiteSpace(returnUrl))
                ViewBag.returnUrl = returnUrl;
            return View("Error404");
        }

        public ActionResult Error401(string returnUrl)
        {
            Response.StatusCode = 401;
            ViewBag.ErrorStatus = "401";
            ViewBag.ErrorMessage = "Yor are not authorised";
            Response.TrySkipIisCustomErrors = true;
            if (!string.IsNullOrWhiteSpace(returnUrl))
                ViewBag.returnUrl = returnUrl;
            return View("Error404");
        }

        #endregion

        public ActionResult MiniProfiler()
        {
            return View();
        }

    }
}