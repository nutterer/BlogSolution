using database.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSolution.Controllers
{
    public partial class BlogController : BaseController
    {
        public ActionResult getDataBlog()
        {
            var Blog = bbc.getBlog();

            return Json(new { data = Blog });
        }
        // GET: Blogbind
        [HttpPost]
        public ActionResult bindSave(tnBlog model)
        {

            var Blog = bbc.bindSave(model);

            return Json(new { data = Blog});
        }
        public ActionResult bindDelete(string BlogID)
        {

            var Blog = bbc.bindDelete(BlogID);
            return Json(new { data = Blog });
        }
        [HttpPost]
        public ActionResult bindEdit(tnBlog model)
        {
            var Blog = bbc.bindEdit(model);

            return Json(new { data = Blog });
        }
    }
}