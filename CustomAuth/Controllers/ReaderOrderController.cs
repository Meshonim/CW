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
            ViewBag.DocumentTemplates = new SelectList(db.OrderReportTemplates, "OrderReportTemplateId", "TemplateName");
            var readerOrders = db.ReaderOrders.Include(r => r.Exemplar).Include(r => r.User);
            if (!User.IsInRole("Admin"))
            {
                var userId = GetCurrentUserId();
                readerOrders = readerOrders.Where(o => o.UserId == userId);
            }
            
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

        [NonAction]
        public List<Exemplar> GetExemplars(int id, int? selfExemplarId)
        {
            var exemplarIds = db.Exemplars.Where(ex => ex.EditionId == id).Select(e => e.ExemplarId).ToList();
            var orders = db.ReaderOrders.Where(o => o.ReaderOrderDateOfIssue <= DateTime.Now && o.ReaderOrderExpiryDate >= DateTime.Now).ToList();
            foreach (var order in orders)
            {
                exemplarIds.Remove(order.ExemplarId);
            }
            if (selfExemplarId.HasValue)
                exemplarIds.Add(selfExemplarId.Value);
            var exemplars = db.Exemplars.Where(e => exemplarIds.Contains(e.ExemplarId)).ToList();
            return exemplars;
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
            ViewBag.EditionId = id.Value;
            ViewBag.ExemplarId = new SelectList(GetExemplars(id.Value, null), "ExemplarId", "ExemplarId");
            var userId = GetCurrentUserId();
            var model = new ReaderOrder()
            {
                UserId = userId,
                ReaderOrderExpiryDate = DateTime.Now
            };
            return View(model);
        }

        // POST: ReaderOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReaderOrderId,ReaderOrderDateOfIssue,ReaderOrderExpiryDate,UserId,ExemplarId")] ReaderOrder readerOrder, int? id)
        {
            bool isValid = true;
            var orders = db.ReaderOrders.Where(o => o.ExemplarId == readerOrder.ExemplarId).ToList();
            foreach (var order in orders)
            {

                if (!(((DateTime.Now < order.ReaderOrderDateOfIssue) && (readerOrder.ReaderOrderExpiryDate < order.ReaderOrderDateOfIssue))
                    ||
                    ((DateTime.Now > order.ReaderOrderExpiryDate) && (readerOrder.ReaderOrderExpiryDate > order.ReaderOrderExpiryDate))))
                    {
                    isValid = false;
                    ModelState.AddModelError(string.Empty, "Book is not available");
                    break;
                }

                    
            }
            if (ModelState.IsValid && readerOrder.ExemplarId != 0 && isValid)
            {
                readerOrder.ReaderOrderDateOfIssue = DateTime.Now;
                db.ReaderOrders.Add(readerOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EditionId = id.Value;
            ViewBag.ExemplarId = new SelectList(GetExemplars(id.Value, null), "ExemplarId", "ExemplarId", readerOrder.ExemplarId);
            return View(readerOrder);
        }

        // GET: ReaderOrder/Edit/5
        public ActionResult Edit(int? id, int? editionId)
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
            ViewBag.EditionId = editionId.Value;
            ViewBag.ExemplarId = new SelectList(GetExemplars(editionId.Value, readerOrder.ExemplarId), "ExemplarId", "ExemplarId", readerOrder.ExemplarId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", readerOrder.UserId);
            return View(readerOrder);
        }

        // POST: ReaderOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReaderOrderId,ReaderOrderDateOfIssue,ReaderOrderExpiryDate,UserId,ExemplarId")] ReaderOrder readerOrder, int? editionId)
        {
            int? eId = null;
            ReaderOrder r = db.ReaderOrders.Find(readerOrder.ReaderOrderId);
            if (r != null)
            {
                eId = r.ExemplarId;
            }
            db.Entry(r).State = EntityState.Detached;
            if (ModelState.IsValid && readerOrder.ExemplarId != 0)
            {
                db.Entry(readerOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EditionId = editionId.Value;
            ViewBag.ExemplarId = new SelectList(GetExemplars(editionId.Value, eId), "ExemplarId", "ExemplarId", readerOrder.ExemplarId);
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

        [NonAction]
        public int GetCurrentUserId()
        {
            var email = User.Identity.Name;
            var userId = 0;
            if (email != null)
            {
                var user = db.Users.Where(u => u.Email == email).FirstOrDefault();
                if (user != null)
                    userId = user.Id;
            }
            return userId;
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
