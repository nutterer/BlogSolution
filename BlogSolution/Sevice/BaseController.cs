using BlogSolution.Models.Bussiness.Connection;
using BlogSolution.Models.ModelApp.Base;
using BlogSolution.Sevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace System.Web.Mvc
{
    public class BaseController : Controller
    {
        public UserCookiesModel UserStatus { get; set; }

        #region --- Constructor ---
        public BaseController()
        {
            GetCookiesAuth();
        }
        #endregion

        #region Manage Cookies
        #region Add Cookies
        public UserCookiesModel AddCookiesAuth(UserCookiesModel model, bool IsRemember = false)
        {
            try
            {
                HttpCookie ckAuth = new HttpCookie("aaaa");

                ckAuth.Values["UserName"] = model.UserName;
                ckAuth.Values["UserID"] = model.UserID;
                ckAuth.Values["MemberType"] = model.MemberType.ToString();

                if (IsRemember)
                    ckAuth.Expires = DateTime.Now.AddDays(365);
                else
                    ckAuth.Expires = DateTime.Now.AddDays(1);

                Response.Cookies.Add(ckAuth);

                return UserStatus;
            }
            catch (Exception ex)
            {
                return UserStatus;
            }
        }
        #endregion
        #region Remove Cookies
        public UserCookiesModel RemoveCookiesAuth()
        {
            try
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
            catch (Exception ex)
            {
                return UserStatus;
            }
        }
        #endregion
        #region Get Cookies
        public UserCookiesModel GetCookiesAuth()
        {
            try
            {
                if (UserStatus == null)
                {
                    ConnectionBC cbc = new ConnectionBC();
                    UserStatus = new UserCookiesModel();
                    HttpCookie ckAuth = System.Web.HttpContext.Current.Request.Cookies["aaaa"];

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
                        RemoveCookiesAuth();
                        UserStatus = null;
                    }
                }
                else
                {
                    AddCookiesAuth(UserStatus);
                }
                ViewBag.UserStatus = UserStatus;
                return UserStatus;
            }
            catch (Exception ex)
            {
                return UserStatus;
            }
        }
        #endregion
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

        #region Set Default Content
        public class SetDefaultContentAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                base.OnActionExecuting(filterContext);
                try
                {
                    var BaseCtl = new BaseSecurityController();
                    BaseCtl.UserStatus = null;
                    BaseCtl.GetCookiesAuthExpire();
                    if (!BaseCtl.checkIsLogin())
                    {
                        //BaseCtl.RememberLastURL();
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "Member",
                            action = "Login",
                            area = ""
                        }));
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        #endregion
    }
}