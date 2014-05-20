using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;
using MvcApp.Models;
using System.Web.Security;
using System.Web.Routing;

namespace MvcApp.Controllers
{
    public class SessionController : Controller
    {
        private lpDBEntities db = new lpDBEntities();



        //
        // GET: /Session/

        [AllowAnonymous]
        public ActionResult LogIn()
        {

            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userdb = db.User.Include("Role").Single(c => c.userName == user.userName);
                    if (userdb.passWord == user.passWord)
                    {
                        Session["UserId"] = userdb.userId;
                        Session["UserName"] = userdb.userName;
                        Session["RoleName"] = userdb.Role.roleName;


                        if (userdb.Role.roleName == "Employee")
                        {
                            return RedirectToAction("Index", "Employee");
                        }
                        else if (userdb.Role.roleName == "Customer")
                        {
                            return RedirectToAction("Index", "CustomerSite");
                        }
                        else if (userdb.Role.roleName == "Admin")
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                    }

                    else
                    {
                        ViewBag.errorMsg = "We cannot authenticate you please use the right username or password!!!";

                    }
                }
                return View();
            }

            catch
            {
                ViewBag.errorMsg = "We cannot authenticate you please use the right username or password!!!!";


                return View();

            }
        }


        public ActionResult Logout()
        {
            Session.Remove("UserId");
            Session.Remove("UserName");
            Session.Remove("RoleName");
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn", "Session");

        }

    }

    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        private bool AuthorizeUser(AuthorizationContext filterContext)
        {
            bool isAuthorized = false;

            if (filterContext.RequestContext.HttpContext != null)
            {
                var context = filterContext.RequestContext.HttpContext;

                if (context.Session["UserId"] != null)
                    isAuthorized = true;
            }
            return isAuthorized;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            if (filterContext == null)
                throw new ArgumentNullException("filterContext");

            if (AuthorizeUser(filterContext))
                return;
            else
            {

                filterContext.Result = new RedirectToRouteResult
                    (new RouteValueDictionary(new { controller = "Session", action = "LogIn" }));

            }


        }
    }

    //public class MyRoleAuthorization : AuthorizeAttribute
    //{
    //    /// <summary>
    //    /// the allowed types
    //    /// </summary>
    //    readonly string[] allowedTypes;

    //    /// <summary>
    //    /// Default constructor with the allowed user types
    //    /// </summary>
    //    /// <param name="allowedTypes"></param>
    //    public MyRoleAuthorization(params string[] allowedTypes)
    //    {
    //        this.allowedTypes = allowedTypes;
    //    }

    //    /// <summary>
    //    /// Gets the allowed types
    //    /// </summary>
    //    public string[] AllowedTypes
    //    {
    //        get { return this.allowedTypes; }
    //    }

    //    /// <summary>
    //    /// Gets the authorize user
    //    /// </summary>
    //    /// <param name="filterContext">the context</param>
    //    /// <returns></returns>
    //    private string AuthorizeUser(AuthorizationContext filterContext)
    //    {
    //        if (filterContext.RequestContext.HttpContext != null)
    //        {
    //            var context = filterContext.RequestContext.HttpContext;
    //            string roleName = Convert.ToString(context.Session["RoleName"]);
    //            switch (roleName)
    //            {
    //                case "Admin":
    //                case "Employee":
    //                case "Customer":
    //                    return roleName;
    //                default:
    //                    throw new ArgumentException("filterContext");
    //            }
    //        }
    //        throw new ArgumentException("filterContext");
    //    }

    //    /// <summary>
    //    /// The authorization override
    //    /// </summary>
    //    /// <param name="filterContext"></param>
    //    public override void OnAuthorization(AuthorizationContext filterContext)
    //    {
    //        if (filterContext == null)
    //            throw new ArgumentException("filterContext");
    //        string authUser = AuthorizeUser(filterContext);
    //        if (!this.AllowedTypes.Any(x => x.Equals(authUser, StringComparison.CurrentCultureIgnoreCase)))
    //        {
    //            filterContext.Result = new HttpUnauthorizedResult();
    //            return;
    //        }
    //    }
    //}
}





