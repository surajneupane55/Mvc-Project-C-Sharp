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
    public class WayOfDeliveryController : Controller
    {
        private lpDBEntities db = new lpDBEntities();

        //
        // GET: /WayOfDelivery/

        public ActionResult Index()
        {
            return View(db.WayOfDelivery.ToList());
        }

        //
        // GET: /WayOfDelivery/Details/5

        public ActionResult Details(int id = 0)
        {
            WayOfDelivery wayofdelivery = db.WayOfDelivery.Find(id);
            if (wayofdelivery == null)
            {
                return HttpNotFound();
            }
            return View(wayofdelivery);
        }

        //
        // GET: /WayOfDelivery/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /WayOfDelivery/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WayOfDelivery wayofdelivery)
        {
            if (ModelState.IsValid)
            {
                db.WayOfDelivery.Add(wayofdelivery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wayofdelivery);
        }

        //
        // GET: /WayOfDelivery/Edit/5

        public ActionResult Edit(int id = 0)
        {
            WayOfDelivery wayofdelivery = db.WayOfDelivery.Find(id);
            if (wayofdelivery == null)
            {
                return HttpNotFound();
            }
            return View(wayofdelivery);
        }

        //
        // POST: /WayOfDelivery/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WayOfDelivery wayofdelivery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wayofdelivery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wayofdelivery);
        }

        //
        // GET: /WayOfDelivery/Delete/5

        public ActionResult Delete(int id = 0)
        {
            WayOfDelivery wayofdelivery = db.WayOfDelivery.Find(id);
            if (wayofdelivery == null)
            {
                return HttpNotFound();
            }
            return View(wayofdelivery);
        }

        //
        // POST: /WayOfDelivery/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WayOfDelivery wayofdelivery = db.WayOfDelivery.Find(id);
            db.WayOfDelivery.Remove(wayofdelivery);
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