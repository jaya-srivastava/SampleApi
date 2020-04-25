using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sample.Service;
using Sample.Models.DTO;
using Sample.Models;
using System.Web.Http.Cors;

namespace IPracticeApi.Controllers
{
    public class HomeController : Controller
    { 
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
