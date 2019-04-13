using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Knjizara.Models;

namespace Knjizara.Controllers
{
    public class ZanrController : Controller
    {
        private KnjizaraDBEntities db = new KnjizaraDBEntities();

        // GET: Zanr
        public ActionResult Index()
        {
            return View("~/Views/Back-end/Zanr/Index.cshtml", db.Zanrs.ToList().OrderBy(z => z.vrsta));
        }
        
        // GET: Zanr/Create
        public ActionResult Create()
        {
            return View("~/Views/Back-end/Zanr/Create.cshtml");
        }

        // POST: Zanr/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_zanr,vrsta")] Zanr zanr)
        {
            if (ModelState.IsValid)
            {
                db.Zanrs.Add(zanr);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("~/Views/Back-end/Zanr/Create.cshtml", zanr);
        }

        // GET: Zanr/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zanr zanr = db.Zanrs.Find(id);
            if (zanr == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Back-end/Zanr/Edit.cshtml", zanr);
        }

        [HttpGet]
        public JsonResult IsGenreExist(string vrsta)
        {
            List<Zanr> zanr = db.Zanrs
                .Where(u => u.vrsta.Equals(vrsta)).ToList();
            
            return Json(zanr.Count == 0, JsonRequestBehavior.AllowGet);
        }

        // POST: Zanr/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_zanr,vrsta")] Zanr zanr)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zanr).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("~/Views/Back-end/Zanr/Edit.cshtml", zanr);
        }

        // GET: Zanr/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zanr zanr = db.Zanrs.Find(id);
            if (zanr == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Back-end/Zanr/Delete.cshtml", zanr);
        }

        // POST: Zanr/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zanr zanr = db.Zanrs.Find(id);
            db.Zanrs.Remove(zanr);
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
