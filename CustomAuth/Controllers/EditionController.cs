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
using CW.ViewModels;
using System.Drawing;
using CW.Helpers;

namespace CW.Controllers
{
    public class EditionController : Controller
    {
        private MainContext db = new MainContext();

        // GET: Edition
        public ActionResult Index()
        {
            var editions = db.Editions
                .Include(e => e.House)
                .Include(e => e.Language)
                .Include(e => e.Genres)
                .Include(e => e.Authors)
                .Include(e => e.Translators)
                .Include(e => e.Illustrators)
                .Include(e => e.EditionType);
            return View(editions.ToList());
        }

        // GET: Edition/Details/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.HouseId = new SelectList(db.Houses, "HouseId", "HouseName");
            ViewBag.LanguageId = new SelectList(db.Languages, "LanguageId", "LanguageName");
            ViewBag.EditionTypeId = new SelectList(db.EditionTypes, "EditionTypeId", "EditionTypeName");
            MultiSelectList genresList = new MultiSelectList(db.Genres.ToList(), "GenreId", "GenreName");
            MultiSelectList authorsList = new MultiSelectList(db.Authors.ToList(), "AuthorId", "FullName");
            MultiSelectList translatorsList = new MultiSelectList(db.Authors.ToList(), "AuthorId", "FullName");
            MultiSelectList illustratorsList = new MultiSelectList(db.Authors.ToList(), "AuthorId", "FullName");

            EditionViewModel model = 
                new EditionViewModel
                {
                    Genres = genresList,
                    Authors = authorsList,
                    Translators = translatorsList,
                    Illustrators = illustratorsList
                };

            return View(model);
        }

        // POST: Edition/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EditionId,EditionTitle,EditionYear,HouseId,LanguageId,EditionTypeId,GenreIds,AuthorIds,TranslatorIds,IllustratorIds")] EditionViewModel model, IEnumerable<HttpPostedFileBase> files)
        {
            if (model.GenreIds == null)
                model.GenreIds = new List<short>();
            if (model.AuthorIds == null)
                model.AuthorIds = new List<int>();
            if (model.TranslatorIds == null)
                model.TranslatorIds = new List<int>();
            if (model.IllustratorIds == null)
                model.IllustratorIds = new List<int>();

            byte[] image = GetImage(files);        

            Edition edition = new Edition()
            {
                EditionTitle = model.EditionTitle,
                EditionYear = model.EditionYear,
                EditionTypeId = model.EditionTypeId,
                HouseId = model.HouseId,
                LanguageId = model.LanguageId,
                EditionImage = image,
                Genres = db.Genres.Where(g => model.GenreIds.Contains(g.GenreId)).ToList(),
                Authors = db.Authors.Where(a => model.AuthorIds.Contains(a.AuthorId)).ToList(),
                Translators = db.Authors.Where(a => model.TranslatorIds.Contains(a.AuthorId)).ToList(),
                Illustrators = db.Authors.Where(a => model.IllustratorIds.Contains(a.AuthorId)).ToList()
            };

            if (ModelState.IsValid)
            {
                db.Editions.Add(edition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HouseId = new SelectList(db.Houses, "HouseId", "HouseName", model.HouseId);
            ViewBag.LanguageId = new SelectList(db.Languages, "LanguageId", "LanguageName", model.LanguageId);
            ViewBag.EditionTypeId = new SelectList(db.EditionTypes, "EditionTypeId", "EditionTypeName", model.EditionTypeId);
            model.Genres = new MultiSelectList(db.Genres, "GenreId", "GenreName", null, model.GenreIds);
            model.Authors = new MultiSelectList(db.Authors, "AuthorId", "FullName", null, model.AuthorIds);
            model.Translators = new MultiSelectList(db.Authors, "AuthorId", "FullName", null, model.TranslatorIds);
            model.Illustrators = new MultiSelectList(db.Authors, "AuthorId", "FullName", null, model.IllustratorIds);
            return View(model);
        }

        // GET: Edition/Edit/5
        [Authorize(Roles = "Admin")]
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
            ViewBag.EditionTypeId = new SelectList(db.EditionTypes, "EditionTypeId", "EditionTypeName", edition.EditionTypeId);
            var model = new EditionViewModel()
            {
                EditionId = edition.EditionId,
                EditionTitle = edition.EditionTitle,
                EditionYear = edition.EditionYear,
                EditionImage = edition.EditionImage,
                EditionTypeId = edition.EditionTypeId,
                HouseId = edition.HouseId,
                LanguageId = edition.LanguageId,
                Genres = new MultiSelectList(db.Genres, "GenreId", "GenreName", null, edition.Genres.Select(g => g.GenreId)),
                Authors = new MultiSelectList(db.Authors, "AuthorId", "FullName", null, edition.Authors.Select(g => g.AuthorId)),
                Translators = new MultiSelectList(db.Authors, "AuthorId", "FullName", null, edition.Translators.Select(g => g.AuthorId)),
                Illustrators = new MultiSelectList(db.Authors, "AuthorId", "FullName", null, edition.Illustrators.Select(g => g.AuthorId))
            };
            return View(model);
        }

        // POST: Edition/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EditionId,EditionTitle,EditionYear,HouseId,LanguageId,EditionTypeId,GenreIds,AuthorIds,TranslatorIds,IllustratorIds")] EditionViewModel model, IEnumerable<HttpPostedFileBase> files)
        {
            if (model.GenreIds == null)
                model.GenreIds = new List<short>();
            if (model.AuthorIds == null)
                model.AuthorIds = new List<int>();
            if (model.TranslatorIds == null)
                model.TranslatorIds = new List<int>();
            if (model.IllustratorIds == null)
                model.IllustratorIds = new List<int>();

            byte[] image = GetImage(files);

            var edition = db.Editions.Find(model.EditionId);

            var item = db.Entry<Edition>(edition);
            item.State = EntityState.Modified;
            item.Collection(i => i.Genres).Load();
            item.Collection(i => i.Authors).Load();
            item.Collection(i => i.Translators).Load();
            item.Collection(i => i.Illustrators).Load();
            edition.EditionTitle = model.EditionTitle;
            edition.EditionYear = model.EditionYear;
            edition.EditionTypeId = model.EditionTypeId;
            edition.HouseId = model.HouseId;
            edition.LanguageId = model.LanguageId;
            if (image != null)
            {
                edition.EditionImage = image;
            }        
            
            edition.Genres = db.Genres.Where(g => model.GenreIds.Contains(g.GenreId)).ToList();
            edition.Authors = db.Authors.Where(g => model.AuthorIds.Contains(g.AuthorId)).ToList();
            edition.Translators = db.Authors.Where(g => model.TranslatorIds.Contains(g.AuthorId)).ToList();
            edition.Illustrators = db.Authors.Where(g => model.IllustratorIds.Contains(g.AuthorId)).ToList();
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            model.EditionImage = edition.EditionImage;
            ViewBag.HouseId = new SelectList(db.Houses, "HouseId", "HouseName", model.HouseId);
            ViewBag.LanguageId = new SelectList(db.Languages, "LanguageId", "LanguageName", model.LanguageId);
            ViewBag.EditionTypeId = new SelectList(db.EditionTypes, "EditionTypeId", "EditionTypeName", model.EditionTypeId);
            model.Genres = new MultiSelectList(db.Genres, "GenreId", "GenreName", null, model.GenreIds);
            model.Authors = new MultiSelectList(db.Authors, "AuthorId", "FullName", null, model.AuthorIds);
            model.Translators = new MultiSelectList(db.Authors, "AuthorId", "FullName", null, model.TranslatorIds);
            model.Illustrators = new MultiSelectList(db.Authors, "AuthorId", "FullName", null, model.IllustratorIds);
            return View(model);
        }

        // GET: Edition/Delete/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Edition edition = db.Editions.Find(id);
            db.Editions.Remove(edition);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [NonAction]
        private byte[] GetImage(IEnumerable<HttpPostedFileBase> files)
        {
            byte[] image = null;
            var file = files as HttpPostedFileBase[] ?? files.ToArray();
            if (file[0] != null)
            {
                using (var img = Image.FromStream(file[0].InputStream))
                {
                    var newSize = new Size(250, 350);
                    var imgSize = ImageHelper.NewImageSize(img.Size, newSize);
                    image = ImageHelper.ImageToByteArray(new Bitmap(img, imgSize.Width, imgSize.Height));
                }
            }
            return image;
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
