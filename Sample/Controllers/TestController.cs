using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using iPractice.Models;
//using iPractice.Repository;

namespace iPractice.Controllers
{ 
	public class TestController : Controller
	{
        //private PracticeContext db = new PracticeContext();

		
		//
		// GET: /Test/

		public ViewResult Index()
		{
			//return View(db.Questions.ToList());
            return View();
		}

		
		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}
	}
}