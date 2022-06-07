using BlogSolution.Models.Bussiness.Connection;
using BlogSolution.Models.ModelApp.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSolution.Sevice
{
    public class BaseSecurityController : Controller
    {
        public UserCookiesModel UserStatus { get; set; }

        #region GetCookiesAuth
        public UserCookiesModel GetCookiesAuth()
        {
            if (UserStatus == null)
            {
                UserStatus = new UserCookiesModel();
                HttpCookie ckAuth = System.Web.HttpContext.Current.Request.Cookies["aaaa"];
                ConnectionBC cbc = new ConnectionBC();

                if (ckAuth != null && cbc.GetConnection() != null)
                {
                    #region UserStatus
                    UserStatus.UserName = ckAuth["UserName"];
                    UserStatus.UserID = ckAuth["UserID"];
                    UserStatus.MemberType = Convert.ToBoolean(ckAuth["MemberType"]);
                    #endregion
                }
                else
                {
                    UserStatus = null;
                }
            }
            ViewBag.UserStatus = UserStatus;
            return UserStatus;
        }
        #endregion

        #region GetCookiesAuth
        public UserCookiesModel GetCookiesAuthExpire()
        {
            ConnectionBC cbc = new ConnectionBC();

            if (UserStatus == null)
            {
                UserStatus = new UserCookiesModel();
                HttpCookie ckAuth = System.Web.HttpContext.Current.Request.Cookies["aaaa"];

                if (!string.IsNullOrEmpty(ckAuth.Value) && ckAuth.Values["UserName"] != "")
                {
                    #region UserStatus
                    UserStatus.UserName = ckAuth["UserName"];
                    UserStatus.UserID = ckAuth["UserID"];
                    UserStatus.MemberType = Convert.ToBoolean(ckAuth["MemberType"]);
                    #endregion 
                }
                else
                {
                    RemoveCookiesAuth();
                    UserStatus = null;
                }
            }
            ViewBag.UserStatus = UserStatus;
            return UserStatus;
        }
        #endregion

        #region CheckIsLogin
        public bool checkIsLogin()
        {
            if (UserStatus != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Remove Cookies
        public UserCookiesModel RemoveCookiesAuth()
        {
            UserStatus = null;
            #region remove userstatus
            HttpCookie ckAuth = new HttpCookie("aaaa");
            ckAuth.Expires = DateTime.Now.AddDays(-1);
            ckAuth.Value = null;
            System.Web.HttpContext.Current.Response.Cookies.Add(ckAuth);
            #endregion
            return UserStatus;
        }
        #endregion
    }
}