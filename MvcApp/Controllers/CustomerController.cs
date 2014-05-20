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
    public class CustomerController : Controller
    {

        private lpDBEntities db = new lpDBEntities();

        //
        // GET: /Customer/
        [MyAuthorizeAttribute]
        public ActionResult Index()
        {
            ViewBag.RoleName = Session["RoleName"];
            var customer = db.Customer.Include(c => c.CustomerGroup).Include(c => c.User);
            return View(customer.ToList());
        }

        //
        // GET: /Customer/Details/5
        [MyAuthorizeAttribute]
        public ActionResult Details(int id = 0)
        {
            ViewBag.RoleName = Session["RoleName"];
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //
        // GET: /Customer/Create
        [MyAuthorizeAttribute]
        public ActionResult Create()
        {
            ViewBag.RoleName = Session["RoleName"];
            ViewBag.customerGroupId = new SelectList(db.CustomerGroup, "customerGroupId", "groupName");
            ViewBag.userId = new SelectList(db.User, "userId", "userName");
            return View();
        }

        //
        // POST: /Customer/Create

        [HttpPost]
        [MyAuthorizeAttribute]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customer.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customerGroupId = new SelectList(db.CustomerGroup, "customerGroupId", "groupName", customer.customerGroupId);
            ViewBag.userId = new SelectList(db.User, "userId", "userName", customer.userId);
            return View(customer);
        }

        //
        // GET: /Customer/Edit/5
        [MyAuthorizeAttribute]
        public ActionResult Edit(int id = 0)
        {
            ViewBag.RoleName = Session["RoleName"];
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.customerGroupId = new SelectList(db.CustomerGroup, "customerGroupId", "groupName", customer.customerGroupId);
            ViewBag.userId = new SelectList(db.User, "userId", "userName", customer.userId);
            return View(customer);
        }

        //
        // POST: /Customer/Edit/5

        [HttpPost]
        [MyAuthorizeAttribute]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customerGroupId = new SelectList(db.CustomerGroup, "customerGroupId", "groupName", customer.customerGroupId);
            ViewBag.userId = new SelectList(db.User, "userId", "userName", customer.userId);
            return View(customer);
        }

        //
        // GET: /Customer/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ViewBag.RoleName = Session["RoleName"];
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //
        // POST: /Customer/Delete/5

        [HttpPost, ActionName("Delete")]
        [MyAuthorizeAttribute]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customer.Find(id);
            db.Customer.Remove(customer);
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