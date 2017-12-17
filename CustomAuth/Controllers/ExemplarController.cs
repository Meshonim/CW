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
using System.Data.SqlClient;
using CW.ViewModels;

namespace CW.Controllers
{
    public class ExemplarController : Controller
    {
        private MainContext db = new MainContext();

        // GET: Exemplar
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var exemplars = db.Exemplars.Include(e => e.Allocation).Include(e => e.Edition);
            return View(exemplars.ToList());
        }

        // GET: Exemplar top
        [Authorize(Roles = "Admin")]
        public ActionResult Top(int? value)
        {
            if (!value.HasValue)
                value = 100;
            var exemplarCostParameter = new SqlParameter("@exemplarCost", value);
            var exemplars = db.Database.SqlQuery<ExemplarViewModel>("dbo.GetExemplarsByCostLevel @exemplarCost", exemplarCostParameter);
            return View(exemplars.OrderByDescending(ex => ex.ExemplarCost).ToList());
        }

        // GET: Exemplar/Details/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.AllocationId = new SelectList(db.Allocations, "AllocationId", "AllocationValue");
            ViewBag.EditionId = new SelectList(db.Editions, "EditionId", "EditionTitle");
            return View();
        }

        // POST: Exemplar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExemplarId,ExemplarCost,EditionId,AllocationId")] Exemplar exemplar)
        {
            if (ModelState.IsValid)
            {
                db.Exemplars.Add(exemplar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AllocationId = new SelectList(db.Allocations, "AllocationId", "AllocationValue", exemplar.AllocationId);
            ViewBag.EditionId = new SelectList(db.Editions, "EditionId", "EditionTitle", exemplar.EditionId);
            return View(exemplar);
        }

        // GET: Exemplar/Edit/5
        [Authorize(Roles = "Admin")]
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
            ViewBag.AllocationId = new SelectList(db.Allocations, "AllocationId", "AllocationValue", exemplar.AllocationId);
            ViewBag.EditionId = new SelectList(db.Editions, "EditionId", "EditionTitle", exemplar.EditionId);
            return View(exemplar);
        }

        // POST: Exemplar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExemplarId,ExemplarCost,EditionId,AllocationId")] Exemplar exemplar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exemplar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AllocationId = new SelectList(db.Allocations, "AllocationId", "AllocationValue", exemplar.AllocationId);
            ViewBag.EditionId = new SelectList(db.Editions, "EditionId", "EditionTitle", exemplar.EditionId);
            return View(exemplar);
        }

        // GET: Exemplar/Delete/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
