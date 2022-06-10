﻿using BlogSolution.Models.Bussiness.Member;
using database.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSolution.Controllers
{
    public partial class MemberController : BaseController
    {
        // GET: Memberbind
        public MemberBC mbc = new MemberBC() ;
        public ActionResult bindlogin(string UserName, string Password, bool IsRemember)
        {
            mbc.isResult = false;
            try
            {
                var user = mbc.bindlogin(UserName, Password);
                if (user != null)
                {
                    AddCookiesAuth(user, IsRemember);
                    mbc.isResult = true;
                }
                return Json(new { isResult = mbc.isResult });
            }
            catch (Exception)
            {
                return Json(new { isResult = mbc.isResult });
            }
        }
        public ActionResult bindlogout ()
        {
            RemoveCookiesAuth();
            return Redirect("~/Member/Login");
        }
        public ActionResult bindregister()
        {
            return View();
        }
        public ActionResult bindEdit(tnMember model)
        {
            var Member = mbc.bindEdit(model);
            return Json(new { data = Member });
        }
        [HttpPost]
        public ActionResult bindSave(tnMember model)
        {

            var Member = mbc.bindSave(model);

            return Json(new { data = Member });
        }
        [HttpPost]
        public ActionResult bindDelete(string MemberID)
        {

            var Member = mbc.bindDelete(MemberID);
            return Json(new { data = Member });
        }
    }
}