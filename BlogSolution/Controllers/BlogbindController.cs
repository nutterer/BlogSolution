using database.Database;
using System;
using System.Collections.Generic;
using System.IO;
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
            if (Request.Files["postedFile"] != null)
            {
                HttpPostedFileBase postedFile = Request.Files["postedFile"];
                string Value = "png";
                string Value1 = "jpg";
                string v = postedFile.ContentType.ToString();
                if (v.Contains(Value) || v.Contains(Value1))
                {
                    string path = Path.Combine(Server.MapPath("~/Uploads"), Path.GetFileName(postedFile.FileName));
                    postedFile.SaveAs(path);
                    model.BlogBannerURl = "/Uploads/" + postedFile.FileName;
                }
                
            }
            
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
            
            if (Request.Files["postedFile"] != null)
            {
                HttpPostedFileBase postedFile = Request.Files["postedFile"];
                string Value = "png";
                string Value1 = "jpg";
                string v = postedFile.ContentType.ToString();
                if (v.Contains(Value) || v.Contains(Value1))
                {
                    string path = Path.Combine(Server.MapPath("~/Uploads"), Path.GetFileName(postedFile.FileName));
                    postedFile.SaveAs(path);
                    model.BlogBannerURl = "/Uploads/" + postedFile.FileName;
                }
                
            }
            var Blog = bbc.bindEdit(model);

            return Json(new { data = Blog });
        }
        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    //Method 2 Get file details from HttpPostedFileBase class    

                    if (file != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                    }
                    ViewBag.FileStatus = "File uploaded successfully.";
                }
                catch (Exception)
                {
                    ViewBag.FileStatus = "Error while file uploading."; ;
                }
            }
            return View("CreateBlog");
        }
    }
}