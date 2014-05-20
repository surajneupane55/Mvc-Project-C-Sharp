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
    public class EmployeeController : Controller
    {
        private lpDBEntities db = new lpDBEntities();
        

        //
        // GET: /Employee/

        [MyAuthorizeAttribute]
        //[MyRoleAuthorization("Employee")]

        public ActionResult Index()
        {
            ViewBag.RoleName = Session["RoleName"];
            var employee = db.Employee.Include(e => e.User);
            return View(employee.ToList());

        }

        //
        // GET: /Employee/Details/5


        [MyAuthorizeAttribute]
        //[MyRoleAuthorization("Employee")]
        public ActionResult Details(int id = 0)
        {
            ViewBag.RoleName = Session["RoleName"];
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //
        // GET: /Employee/Create


        [MyAuthorizeAttribute]
        //[MyRoleAuthorization("Employee")]
        public ActionResult Create()
        {
            ViewBag.RoleName = Session["RoleName"];
            ViewBag.userId = new SelectList(db.User, "userId", "userName");
            return View();
        }

        //
        // POST: /Employee/Create

        [HttpPost]

        [MyAuthorizeAttribute]
        //[MyRoleAuthorization("Employee")]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employee.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userId = new SelectList(db.User, "userId", "userName", employee.userId);
            return View(employee);
        }

        //
        // GET: /Employee/Edit/5


        [MyAuthorizeAttribute]
        //[MyRoleAuthorization("Employee")]
        public ActionResult Edit(int id = 0)
        {
            ViewBag.RoleName = Session["RoleName"];
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = new SelectList(db.User, "userId", "userName", employee.userId);
            return View(employee);
        }

        //
        // POST: /Employee/Edit/5

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userId = new SelectList(db.User, "userId", "userName", employee.userId);
            return View(employee);
        }

        //
        // GET: /Employee/Delete/5


        [MyAuthorizeAttribute]
        //[MyRoleAuthorization("Employee")]
        public ActionResult Delete(int id = 0)
        {
            ViewBag.RoleName = Session["RoleName"];
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //
        // POST: /Employee/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employee.Find(id);
            db.Employee.Remove(employee);
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