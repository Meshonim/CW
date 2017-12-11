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
    public class RuleController : Controller
    {
        private MainContext db = new MainContext();
        private List<object> rulesList = new List<object>()
        {
                new { Text = "1", Value = "1" },
                new { Text = "2", Value = "2" },
                new { Text = "3", Value = "3" },
        };

        // GET: Rule
        public ActionResult Index()
        {
            return View(db.Rules.OrderBy(r => r.RulePriority).ToList());
        }

        // GET: Rule/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DalToWeb.Models.Rule rule = db.Rules.Find(id);
            if (rule == null)
            {
                return HttpNotFound();
            }
            return View(rule);
        }

        // GET: Rule/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.Rul = new SelectList(rulesList, "Value", "Text");
            return View();
        }

        // POST: Rule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RuleId,RuleValue,RulePriority")] DalToWeb.Models.Rule rule)
        {
            if (ModelState.IsValid)
            {
                db.Rules.Add(rule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Rul = new SelectList(rulesList, "Value", "Text", rule.RulePriority);
            return View(rule);
        }

        // GET: Rule/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DalToWeb.Models.Rule rule = db.Rules.Find(id);
            if (rule == null)
            {
                return HttpNotFound();
            }
            ViewBag.Rul = new SelectList(rulesList, "Value", "Text", rule.RulePriority);
            return View(rule);
        }

        // POST: Rule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RuleId,RuleValue,RulePriority")] DalToWeb.Models.Rule rule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Rul = new SelectList(rulesList, "Value", "Text", rule.RulePriority);
            return View(rule);
        }

        // GET: Rule/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DalToWeb.Models.Rule rule = db.Rules.Find(id);
            if (rule == null)
            {
                return HttpNotFound();
            }
            return View(rule);
        }

        // POST: Rule/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DalToWeb.Models.Rule rule = db.Rules.Find(id);
            db.Rules.Remove(rule);
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
