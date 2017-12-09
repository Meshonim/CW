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
    public class EditionController : Controller
    {
        private MainContext db = new MainContext();

        // GET: Edition
        public ActionResult Index()
        {
            var editions = db.Editions.Include(e => e.House).Include(e => e.Language);
            return View(editions.ToList());
        }

        // GET: Edition/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Edition edition = db.Editions.Find(id);
            if (edition == null)
            {
                return HttpNotFound();
            }
            return View(edition);
        }

        // GET: Edition/Create
        public ActionResult Create()
        {
            ViewBag.HouseId = new SelectList(db.Houses, "HouseId", "HouseName");
            ViewBag.LanguageId = new SelectList(db.Languages, "LanguageId", "LanguageName");
            return View();
        }

        // POST: Edition/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EditionId,EditionTitle,EditionYear,HouseId,LanguageId")] Edition edition)
        {
            if (ModelState.IsValid)
            {
                db.Editions.Add(edition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HouseId = new SelectList(db.Houses, "HouseId", "HouseName", edition.HouseId);
            ViewBag.LanguageId = new SelectList(db.Languages, "LanguageId", "LanguageName", edition.LanguageId);
            return View(edition);
        }

        // GET: Edition/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Edition edition = db.Editions.Find(id);
            if (edition == null)
            {
                return HttpNotFound();
            }
            ViewBag.HouseId = new SelectList(db.Houses, "HouseId", "HouseName", edition.HouseId);
            ViewBag.LanguageId = new SelectList(db.Languages, "LanguageId", "LanguageName", edition.LanguageId);
            return View(edition);
        }

        // POST: Edition/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EditionId,EditionTitle,EditionYear,HouseId,LanguageId")] Edition edition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(edition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HouseId = new SelectList(db.Houses, "HouseId", "HouseName", edition.HouseId);
            ViewBag.LanguageId = new SelectList(db.Languages, "LanguageId", "LanguageName", edition.LanguageId);
            return View(edition);
        }

        // GET: Edition/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Edition edition = db.Editions.Find(id);
            if (edition == null)
            {
                return HttpNotFound();
            }
            return View(edition);
        }

        // POST: Edition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Edition edition = db.Editions.Find(id);
            db.Editions.Remove(edition);
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
