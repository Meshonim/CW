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
    public class ExemplarController : Controller
    {
        private MainContext db = new MainContext();

        // GET: Exemplar
        public ActionResult Index()
        {
            var exemplars = db.Exemplars.Include(e => e.Edition);
            return View(exemplars.ToList());
        }

        // GET: Exemplar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exemplar exemplar = db.Exemplars.Find(id);
            if (exemplar == null)
            {
                return HttpNotFound();
            }
            return View(exemplar);
        }

        // GET: Exemplar/Create
        public ActionResult Create()
        {
            ViewBag.EditionId = new SelectList(db.Editions, "EditionId", "EditionTitle");
            return View();
        }

        // POST: Exemplar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExemplarId,ExemplarCost,EditionId")] Exemplar exemplar)
        {
            if (ModelState.IsValid)
            {
                db.Exemplars.Add(exemplar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EditionId = new SelectList(db.Editions, "EditionId", "EditionTitle", exemplar.EditionId);
            return View(exemplar);
        }

        // GET: Exemplar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exemplar exemplar = db.Exemplars.Find(id);
            if (exemplar == null)
            {
                return HttpNotFound();
            }
            ViewBag.EditionId = new SelectList(db.Editions, "EditionId", "EditionTitle", exemplar.EditionId);
            return View(exemplar);
        }

        // POST: Exemplar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExemplarId,ExemplarCost,EditionId")] Exemplar exemplar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exemplar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EditionId = new SelectList(db.Editions, "EditionId", "EditionTitle", exemplar.EditionId);
            return View(exemplar);
        }

        // GET: Exemplar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exemplar exemplar = db.Exemplars.Find(id);
            if (exemplar == null)
            {
                return HttpNotFound();
            }
            return View(exemplar);
        }

        // POST: Exemplar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Exemplar exemplar = db.Exemplars.Find(id);
            db.Exemplars.Remove(exemplar);
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
