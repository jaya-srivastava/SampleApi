using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using iPractice.Repository;

namespace iPractice.Controllers
{
	public class XmlController : Controller
	{
		//
		// GET: /Xml/

        private MainRepository rp = new MainRepository();

		public ActionResult Index()
		{
			return View();
		}

		public ContentResult Sitemap()
		{
			XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
			//const string url = "http://www.iPracticeMath.com/Grades/Show/{0}/{1}";
			var items = rp.GetTopics();
			var sitemap = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
				new XElement(ns + "urlset",
					from item in items
					select
					new XElement(ns + "url",
					  //new XElement(ns+ "loc", string.Format(url, item.ID, UrlTidy.ToCleanUrl(item.Head))),
					  //item.DateAmended != null ?
					  //	new XElement(ns + "lastmod", String.Format("{0:yyyy-MM-dd}", item.DateAmended)) :
					  //	new XElement(ns + "lastmod", String.Format("{0:yyyy-MM-dd}", item.DateCreated)),
					  new XElement(ns + "changefreq", "monthly"),
					  new XElement(ns + "priority", "0.5")
					  )
					)
				  );
			return Content(sitemap.ToString(), "text/xml");
		}


	}
}
