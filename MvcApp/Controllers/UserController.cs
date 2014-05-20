using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    public class UserController : Controller
    {
        private lpDBEntities db = new lpDBEntities();

        //
        // GET: /User/

        [MyAuthorizeAttribute]
        public ActionResult Index()
        {

            ViewBag.RoleName = Session["RoleName"];
            var user = db.User.Include(u => u.Customer).Include(u => u.Role);
            return View(user.ToList());
        }
        //
        // GET: /User/Details/5

        [MyAuthorizeAttribute]
        public ActionResult Details(int id = 0)
        {

            //DetailView detailview= = new DetailView() {User = objuser, Shoes= objShoes }

            User user = db.User.Find(id);
            string rolename = user.Role.roleName;



            if (user == null)
            {
                return HttpNotFound();
            }
            try
            {
                if (rolename == "Admin")
                {
                    ViewBag.rolename = "admin";
                    ViewBag.CurrentRole = Session["RoleName"];
                    DetailView Detailview = new DetailView();


                    Admin admin = db.Admin.Single(c => c.userId == user.userId);
                    return View("Details", new DetailView { Admin = admin, User = user });
                }

                else if (rolename == "Employee")
                {
                    ViewBag.CurrentRole = Session["RoleName"];
                    ViewBag.rolename = "emp";
                    DetailView Detailview = new DetailView();
                    Employee employee = db.Employee.Single(c => c.userId == user.userId);


                    return View("Details", new DetailView { Employee = employee, User = user });


                }

                else if (rolename == "Customer")
                {
                    ViewBag.CurrentRole = Session["RoleName"];
                    ViewBag.rolename = "customer";
                    DetailView Detailview = new DetailView();

                    Customer customer = db.Customer.Single(c => c.userId == user.userId);

                    return View("Details", new DetailView { Customer = customer, User = user });

                }



                else
                {
                    return HttpNotFound();

                }
            }
            catch
            {
                return HttpNotFound();
            }



        }


        //
        // GET: /User/Create

        [MyAuthorizeAttribute]
        public ActionResult Create()
        {
            ViewBag.RoleName = Session["RoleName"];
            ViewBag.userId = new SelectList(db.Customer, "userId", "alphabeticId");
            ViewBag.roleId = new SelectList(db.Role, "roleId", "roleName");
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.User.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userId = new SelectList(db.Customer, "userId", "alphabeticId", user.userId);
            ViewBag.roleId = new SelectList(db.Role, "roleId", "roleName", user.roleId);
            return View(user);
        }

        //
        // GET: /User/Edit/5
        [MyAuthorizeAttribute]
        public ActionResult Edit(int id = 0)
        {
            ViewBag.RoleName = Session["RoleName"];
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = new SelectList(db.Customer, "userId", "alphabeticId", user.userId);
            ViewBag.roleId = new SelectList(db.Role, "roleId", "roleName", user.roleId);
            return View(user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userId = new SelectList(db.Customer, "userId", "alphabeticId", user.userId);
            ViewBag.roleId = new SelectList(db.Role, "roleId", "roleName", user.roleId);
            return View(user);
        }

        //
        // GET: /User/Delete/5

        [MyAuthorizeAttribute]
        public ActionResult Delete(int id = 0)
        {
            ViewBag.RoleName = Session["RoleName"];
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.User.Find(id);
            db.User.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}