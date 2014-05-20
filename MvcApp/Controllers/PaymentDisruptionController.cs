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
    public class PaymentDisruptionController : Controller
    {
        private lpDBEntities db = new lpDBEntities();

        //
        // GET: /PaymentDisruption/

        public ActionResult Index()
        {
            return View(db.PaymentDisruption.ToList());
        }

        //
        // GET: /PaymentDisruption/Details/5

        public ActionResult Details(int id = 0)
        {
            PaymentDisruption paymentdisruption = db.PaymentDisruption.Find(id);
            if (paymentdisruption == null)
            {
                return HttpNotFound();
            }
            return View(paymentdisruption);
        }

        //
        // GET: /PaymentDisruption/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PaymentDisruption/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaymentDisruption paymentdisruption)
        {
            if (ModelState.IsValid)
            {
                db.PaymentDisruption.Add(paymentdisruption);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paymentdisruption);
        }

        //
        // GET: /PaymentDisruption/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PaymentDisruption paymentdisruption = db.PaymentDisruption.Find(id);
            if (paymentdisruption == null)
            {
                return HttpNotFound();
            }
            return View(paymentdisruption);
        }

        //
        // POST: /PaymentDisruption/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PaymentDisruption paymentdisruption)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentdisruption).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paymentdisruption);
        }

        //
        // GET: /PaymentDisruption/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PaymentDisruption paymentdisruption = db.PaymentDisruption.Find(id);
            if (paymentdisruption == null)
            {
                return HttpNotFound();
            }
            return View(paymentdisruption);
        }

        //
        // POST: /PaymentDisruption/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentDisruption paymentdisruption = db.PaymentDisruption.Find(id);
            db.PaymentDisruption.Remove(paymentdisruption);
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