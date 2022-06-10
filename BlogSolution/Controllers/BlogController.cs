using BlogSolution.Models.Bussiness.Blog;
using BlogSolution.Models.Bussiness.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSolution.Controllers
{
    public partial class BlogController : BaseController
    {
        public BlogBC bbc = new BlogBC();
        public MemberBC mbc = new MemberBC();
        // GET: Blog
        [SetDefaultContent]
        public ActionResult BlogCreate()
        {
            var idd = UserStatus.MemberID;
            ViewBag.user = mbc.getMember(idd).FirstOrDefault();
            return View();
        }
        [SetDefaultContent]
        public ActionResult FeedBlog()
        {
            
            string BlogID = "";
            ViewBag.Blog = bbc.getBlog(BlogID);
            return View();
        }
        [SetDefaultContent]
        public ActionResult  EditFeed(string id)
        {
            
            ViewBag.Blog = bbc.getBlog(id).FirstOrDefault();
            return View();
        }
        [SetDefaultContent]
        public ActionResult Detail(string id ,string idd)
        {
           
            ViewBag.user = mbc.getMember(idd).FirstOrDefault();
            ViewBag.Blog = bbc.getBlog(id).FirstOrDefault();
            return View();
        }
    } 
}