using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DalToWeb.Models;
using DalToWeb.Repositories;

namespace CW.Controllers
{
    public class ReaderOrderController : Controller
    {
        private MainContext db = new MainContext();

        // GET: ReaderOrder
        public ActionResult Index()
        {
            var readerOrders = db.ReaderOrders.Include(r => r.Exemplar).Include(r => r.User);
            return View(readerOrders.ToList());
        }

        // GET: ReaderOrder/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReaderOrder readerOrder = db.ReaderOrders.Find(id);
            if (readerOrder == null)
            {
                return HttpNotFound();
            }
            return View(readerOrder);
        }

        // GET: ReaderOrder/Create
        [Authorize]
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var edition = db.Editions.Find(id);
            if (edition == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExemplarId = new SelectList(db.Exemplars.Where(ex => ex.EditionId == id), "ExemplarId", "ExemplarId");
            var email = User.Identity.Name;
            var userId = 0;
            if (email != null)
            {
                userId = db.Users.Where(u => u.Email == email).First().Id;
            }
            var model = new ReaderOrder()
            {
                UserId = userId
            };
            return View(model);
        }

        // POST: ReaderOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReaderOrderId,ReaderOrderDateOfIssue,ReaderOrderExpiryDate,UserId,ExemplarId")] ReaderOrder readerOrder)
        {
            if (ModelState.IsValid)
            {
                readerOrder.ReaderOrderDateOfIssue = DateTime.Now;
                db.ReaderOrders.Add(readerOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExemplarId = new SelectList(db.Exemplars, "ExemplarId", "ExemplarId", readerOrder.ExemplarId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", readerOrder.UserId);
            return View(readerOrder);
        }

        // GET: ReaderOrder/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReaderOrder readerOrder = db.ReaderOrders.Find(id);
            if (readerOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExemplarId = new SelectList(db.Exemplars, "ExemplarId", "ExemplarId", readerOrder.ExemplarId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", readerOrder.UserId);
            return View(readerOrder);
        }

        // POST: ReaderOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReaderOrderId,ReaderOrderDateOfIssue,ReaderOrderExpiryDate,UserId,ExemplarId")] ReaderOrder readerOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(readerOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExemplarId = new SelectList(db.Exemplars, "ExemplarId", "ExemplarId", readerOrder.ExemplarId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", readerOrder.UserId);
            return View(readerOrder);
        }

        // GET: ReaderOrder/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReaderOrder readerOrder = db.ReaderOrders.Find(id);
            if (readerOrder == null)
            {
                return HttpNotFound();
            }
            return View(readerOrder);
        }

        // POST: ReaderOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReaderOrder readerOrder = db.ReaderOrders.Find(id);
            db.ReaderOrders.Remove(readerOrder);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
