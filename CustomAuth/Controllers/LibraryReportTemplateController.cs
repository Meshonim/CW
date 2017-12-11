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
    public class LibraryReportTemplateController : Controller
    {
        private MainContext db = new MainContext();

        // GET: LibraryReportTemplate
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.LibraryReportTemplates.ToList());
        }

        // GET: LibraryReportTemplate/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LibraryReportTemplate libraryReportTemplate = db.LibraryReportTemplates.Find(id);
            if (libraryReportTemplate == null)
            {
                return HttpNotFound();
            }
            return View(libraryReportTemplate);
        }

        // GET: LibraryReportTemplate/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: LibraryReportTemplate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LibraryReportTemplateId,TemplateName,PrintHeaders,Autosized,MaxCount")] LibraryReportTemplate libraryReportTemplate)
        {
            if (ModelState.IsValid)
            {
                db.LibraryReportTemplates.Add(libraryReportTemplate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(libraryReportTemplate);
        }

        // GET: LibraryReportTemplate/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LibraryReportTemplate libraryReportTemplate = db.LibraryReportTemplates.Find(id);
            if (libraryReportTemplate == null)
            {
                return HttpNotFound();
            }
            return View(libraryReportTemplate);
        }

        // POST: LibraryReportTemplate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LibraryReportTemplateId,TemplateName,PrintHeaders,Autosized,MaxCount")] LibraryReportTemplate libraryReportTemplate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(libraryReportTemplate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(libraryReportTemplate);
        }

        // GET: LibraryReportTemplate/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LibraryReportTemplate libraryReportTemplate = db.LibraryReportTemplates.Find(id);
            if (libraryReportTemplate == null)
            {
                return HttpNotFound();
            }
            return View(libraryReportTemplate);
        }

        // POST: LibraryReportTemplate/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LibraryReportTemplate libraryReportTemplate = db.LibraryReportTemplates.Find(id);
            db.LibraryReportTemplates.Remove(libraryReportTemplate);
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
