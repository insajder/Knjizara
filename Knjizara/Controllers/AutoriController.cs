using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Knjizara.Models;

namespace Knjizara.Controllers
{
    public class AutoriController : Controller
    {
        private KnjizaraDBEntities db = new KnjizaraDBEntities();

        // GET: Autori
        public ActionResult Index()
        {
            return View("~/Views/Back-end/Autori/Index.cshtml", db.Autoris.ToList().OrderBy(a => a.ime));
        }

        // GET: Autori/Create
        public ActionResult Create()
        {
            return View("~/Views/Back-end/Autori/Create.cshtml");
        }

        // POST: Autori/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_autora,ime")] Autori autori)
        {
            if (ModelState.IsValid)
            {
                db.Autoris.Add(autori);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("~/Views/Back-end/Autori/Create.cshtml", autori);
        }

        // GET: Autori/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autori autori = db.Autoris.Find(id);
            if (autori == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/back-end/Autori/Edit.cshtml", autori);
        }

        // POST: Autori/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_autora,ime")] Autori autori)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autori).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("~/Views/Back-end/Autori/Edit.cshtml", autori);
        }

        // GET: Autori/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autori autori = db.Autoris.Find(id);
            if (autori == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Back-end/Autori/Delete.cshtml", autori);
        }

        // POST: Autori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Autori autori = db.Autoris.Find(id);
            db.Autoris.Remove(autori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult IsAuthorExist(string ime)
        {
            List<Autori> autor = db.Autoris
                .Where(u => u.ime.Equals(ime)).ToList();

            return Json(autor.Count == 0, JsonRequestBehavior.AllowGet);
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
