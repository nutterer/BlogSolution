using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSolution.Controllers
{
    public class HomeController : BaseController
    {
        [SetDefaultContent]
        public ActionResult Index()
        {
            return View();
        }
        [SetDefaultContent]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [SetDefaultContent]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}