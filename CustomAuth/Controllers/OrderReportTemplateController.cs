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
    public class OrderReportTemplateController : Controller
    {
        private MainContext db = new MainContext();

        // GET: OrderReportTemplate
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.OrderReportTemplates.ToList());
        }

        // GET: OrderReportTemplate/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderReportTemplate orderReportTemplate = db.OrderReportTemplates.Find(id);
            if (orderReportTemplate == null)
            {
                return HttpNotFound();
            }
            return View(orderReportTemplate);
        }

        // GET: OrderReportTemplate/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderReportTemplate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderReportTemplateId,TemplateName,PrintBackground,PrintDayOfWeek,PrintEditionTitle,MaxCount")] OrderReportTemplate orderReportTemplate)
        {
            if (ModelState.IsValid)
            {
                db.OrderReportTemplates.Add(orderReportTemplate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderReportTemplate);
        }

        // GET: OrderReportTemplate/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderReportTemplate orderReportTemplate = db.OrderReportTemplates.Find(id);
            if (orderReportTemplate == null)
            {
                return HttpNotFound();
            }
            return View(orderReportTemplate);
        }

        // POST: OrderReportTemplate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderReportTemplateId,TemplateName,PrintBackground,PrintDayOfWeek,PrintEditionTitle,MaxCount")] OrderReportTemplate orderReportTemplate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderReportTemplate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderReportTemplate);
        }

        // GET: OrderReportTemplate/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderReportTemplate orderReportTemplate = db.OrderReportTemplates.Find(id);
            if (orderReportTemplate == null)
            {
                return HttpNotFound();
            }
            return View(orderReportTemplate);
        }

        // POST: OrderReportTemplate/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderReportTemplate orderReportTemplate = db.OrderReportTemplates.Find(id);
            db.OrderReportTemplates.Remove(orderReportTemplate);
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
