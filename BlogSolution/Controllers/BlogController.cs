using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSolution.Controllers
{
    public class BlogController : BaseController
    {
        // GET: Blog
        [SetDefaultContent]
        public ActionResult BlogCreate()
        {
            return View();
        }
    }
}