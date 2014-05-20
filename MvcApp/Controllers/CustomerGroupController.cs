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
    public class CustomerGroupController : Controller
    {
        private lpDBEntities db = new lpDBEntities();

        //
        // GET: /CustomerGroup/

        public ActionResult Index()
        {
            return View(db.CustomerGroup.ToList());
        }

        //
        // GET: /CustomerGroup/Details/5

        public ActionResult Details(int id = 0)
        {
            CustomerGroup customergroup = db.CustomerGroup.Find(id);
            if (customergroup == null)
            {
                return HttpNotFound();
            }
            return View(customergroup);
        }

        //
        // GET: /CustomerGroup/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CustomerGroup/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerGroup customergroup)
        {
            if (ModelState.IsValid)
            {
                db.CustomerGroup.Add(customergroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customergroup);
        }

        //
        // GET: /CustomerGroup/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CustomerGroup customergroup = db.CustomerGroup.Find(id);
            if (customergroup == null)
            {
                return HttpNotFound();
            }
            return View(customergroup);
        }

        //
        // POST: /CustomerGroup/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerGroup customergroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customergroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customergroup);
        }

        //
        // GET: /CustomerGroup/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CustomerGroup customergroup = db.CustomerGroup.Find(id);
            if (customergroup == null)
            {
                return HttpNotFound();
            }
            return View(customergroup);
        }

        //
        // POST: /CustomerGroup/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerGroup customergroup = db.CustomerGroup.Find(id);
            db.CustomerGroup.Remove(customergroup);
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