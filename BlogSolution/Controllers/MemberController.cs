using BlogSolution.Models.Bussiness.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSolution.Controllers
{
    public partial class MemberController : BaseController
    {
        // GET: Member
        public MemberBC bc = new MemberBC();
        [SetDefaultContent]
        public ActionResult MemberList()

        {
            var MemberID = "";
            
            ViewBag.Members = bc.getMember(MemberID);

            return View();
        }
        
        public ActionResult Login()
        {
            
            return View();
        }
       
        public ActionResult Register(string id)
        {
            ViewBag.user = mbc.getMember(id).FirstOrDefault();
            return View();
        }
        [SetDefaultContent]
        public ActionResult EditMember(string id)
        {
            ViewBag.user = mbc.getMember(id).FirstOrDefault();
            return View();
            
        }
    }
}