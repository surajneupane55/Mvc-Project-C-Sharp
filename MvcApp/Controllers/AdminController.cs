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
    public class AdminController : Controller
    {
        private lpDBEntities db = new lpDBEntities();

        //
        // GET: /Admin/

        [MyAuthorizeAttribute]
        public ActionResult Index()
        {
            ViewBag.RoleName = Session["RoleName"];
            var admin = db.Admin.Include(a => a.User.Admin);
            return View(admin.ToList());
        }

        //
        // GET: /Admin/Details/5
        [MyAuthorizeAttribute]

        public ActionResult Details(int id = 0)
        {
            ViewBag.RoleName = Session["RoleName"];
            Admin admin = db.Admin.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        //
        // GET: /Admin/Create
        [MyAuthorizeAttribute]

        public ActionResult Create()
        {
            ViewBag.RoleName = Session["RoleName"];
            ViewBag.userId = new SelectList(db.User, "userId", "userName");
            return View();
        }

        //
        // POST: /Admin/Create

        [HttpPost]
        public ActionResult Create(Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Admin.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userId = new SelectList(db.User, "userId", "userName", admin.userId);
            return View(admin);
        }

        //
        // GET: /Admin/Edit/5

        [MyAuthorizeAttribute]

        public ActionResult Edit(int id = 0)
        {
            ViewBag.RoleName = Session["RoleName"];
            Admin admin = db.Admin.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = new SelectList(db.User, "userId", "userName", admin.userId);
            return View(admin);
        }

        //
        // POST: /Admin/Edit/5


        [HttpPost]
        public ActionResult Edit(Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userId = new SelectList(db.User, "userId", "userName", admin.userId);
            return View(admin);
        }

        //
        // GET: /Admin/Delete/5

        [MyAuthorizeAttribute]

        public ActionResult Delete(int id = 0)
        {
            Admin admin = db.Admin.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Admin.Find(id);
            db.Admin.Remove(admin);
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