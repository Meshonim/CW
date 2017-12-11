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
    public class LibraryOrderController : Controller
    {
        private MainContext db = new MainContext();

        // GET: LibraryOrder
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var libraryOrders = db.LibraryOrders.Include(l => l.Edition);
            return View(libraryOrders.ToList());
        }

        // GET: LibraryOrder/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LibraryOrder libraryOrder = db.LibraryOrders.Find(id);
            if (libraryOrder == null)
            {
                return HttpNotFound();
            }
            return View(libraryOrder);
        }

        // GET: LibraryOrder/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.EditionId = new SelectList(db.Editions, "EditionId", "EditionTitle");
            return View();
        }

        // POST: LibraryOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LibraryOrderId,LibraryOrderStatus,LibraryOrderCount,EditionId")] LibraryOrder libraryOrder)
        {
            if (ModelState.IsValid)
            {
                db.LibraryOrders.Add(libraryOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EditionId = new SelectList(db.Editions, "EditionId", "EditionTitle", libraryOrder.EditionId);
            return View(libraryOrder);
        }

        // GET: LibraryOrder/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LibraryOrder libraryOrder = db.LibraryOrders.Find(id);
            if (libraryOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.EditionId = new SelectList(db.Editions, "EditionId", "EditionTitle", libraryOrder.EditionId);
            return View(libraryOrder);
        }

        // POST: LibraryOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LibraryOrderId,LibraryOrderStatus,LibraryOrderCount,EditionId")] LibraryOrder libraryOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(libraryOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EditionId = new SelectList(db.Editions, "EditionId", "EditionTitle", libraryOrder.EditionId);
            return View(libraryOrder);
        }

        // GET: LibraryOrder/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LibraryOrder libraryOrder = db.LibraryOrders.Find(id);
            if (libraryOrder == null)
            {
                return HttpNotFound();
            }
            return View(libraryOrder);
        }

        // POST: LibraryOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LibraryOrder libraryOrder = db.LibraryOrders.Find(id);
            db.LibraryOrders.Remove(libraryOrder);
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
