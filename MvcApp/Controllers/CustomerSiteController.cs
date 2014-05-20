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
    public class CustomerSiteController : Controller
    {
        private lpDBEntities db = new lpDBEntities();

        //
        // GET: /CustomerSite/

        public ActionResult Index()
        {
            var customersite = db.CustomerSite.Include(c => c.Customer).Include(c => c.PaymentDisruption).Include(c => c.WayOfDelivery);
            return View(customersite.ToList());
        }

        //
        // GET: /CustomerSite/Details/5

        public ActionResult Details(int id = 0)
        {
            CustomerSite customersite = db.CustomerSite.Find(id);
            if (customersite == null)
            {
                return HttpNotFound();
            }
            return View(customersite);
        }

        //
        // GET: /CustomerSite/Create

        public ActionResult Create()
        {
            ViewBag.customerId = new SelectList(db.Customer, "userId", "alphabeticId");
            ViewBag.paymentDisruptionId = new SelectList(db.PaymentDisruption, "paymentDisruptionId", "paymentDisruptionType");
            ViewBag.wayOfDeliveryId = new SelectList(db.WayOfDelivery, "wayOfDeliveryId", "deliveryText");
            return View();
        }

        //
        // POST: /CustomerSite/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerSite customersite)
        {
            if (ModelState.IsValid)
            {
                db.CustomerSite.Add(customersite);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customerId = new SelectList(db.Customer, "userId", "alphabeticId", customersite.customerId);
            ViewBag.paymentDisruptionId = new SelectList(db.PaymentDisruption, "paymentDisruptionId", "paymentDisruptionType", customersite.paymentDisruptionId);
            ViewBag.wayOfDeliveryId = new SelectList(db.WayOfDelivery, "wayOfDeliveryId", "deliveryText", customersite.wayOfDeliveryId);
            return View(customersite);
        }

        //
        // GET: /CustomerSite/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CustomerSite customersite = db.CustomerSite.Find(id);
            if (customersite == null)
            {
                return HttpNotFound();
            }
            ViewBag.customerId = new SelectList(db.Customer, "userId", "alphabeticId", customersite.customerId);
            ViewBag.paymentDisruptionId = new SelectList(db.PaymentDisruption, "paymentDisruptionId", "paymentDisruptionType", customersite.paymentDisruptionId);
            ViewBag.wayOfDeliveryId = new SelectList(db.WayOfDelivery, "wayOfDeliveryId", "deliveryText", customersite.wayOfDeliveryId);
            return View(customersite);
        }

        //
        // POST: /CustomerSite/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerSite customersite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customersite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customerId = new SelectList(db.Customer, "userId", "alphabeticId", customersite.customerId);
            ViewBag.paymentDisruptionId = new SelectList(db.PaymentDisruption, "paymentDisruptionId", "paymentDisruptionType", customersite.paymentDisruptionId);
            ViewBag.wayOfDeliveryId = new SelectList(db.WayOfDelivery, "wayOfDeliveryId", "deliveryText", customersite.wayOfDeliveryId);
            return View(customersite);
        }

        //
        // GET: /CustomerSite/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CustomerSite customersite = db.CustomerSite.Find(id);
            if (customersite == null)
            {
                return HttpNotFound();
            }
            return View(customersite);
        }

        //
        // POST: /CustomerSite/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerSite customersite = db.CustomerSite.Find(id);
            db.CustomerSite.Remove(customersite);
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