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
        public ActionResult bindregister()
        {
            return View();
        }
    }
}