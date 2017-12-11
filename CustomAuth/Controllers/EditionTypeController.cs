﻿using System;
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
    public class EditionTypeController : Controller
    {
        private MainContext db = new MainContext();

        // GET: EditionType
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.EditionTypes.ToList());
        }

        // GET: EditionType/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditionType editionType = db.EditionTypes.Find(id);
            if (editionType == null)
            {
                return HttpNotFound();
            }
            return View(editionType);
        }

        // GET: EditionType/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: EditionType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EditionTypeId,EditionTypeName,EditionTypeDescription")] EditionType editionType)
        {
            if (ModelState.IsValid)
            {
                db.EditionTypes.Add(editionType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(editionType);
        }

        // GET: EditionType/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditionType editionType = db.EditionTypes.Find(id);
            if (editionType == null)
            {
                return HttpNotFound();
            }
            return View(editionType);
        }

        // POST: EditionType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EditionTypeId,EditionTypeName,EditionTypeDescription")] EditionType editionType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(editionType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editionType);
        }

        // GET: EditionType/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditionType editionType = db.EditionTypes.Find(id);
            if (editionType == null)
            {
                return HttpNotFound();
            }
            return View(editionType);
        }

        // POST: EditionType/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EditionType editionType = db.EditionTypes.Find(id);
            db.EditionTypes.Remove(editionType);
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
