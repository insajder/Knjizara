using Knjizara.Models;
using Knjizara.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Knjizara.Controllers
{
    public class AdminNarudzbinaController : Controller
    {
        private KnjizaraDBEntities db = new KnjizaraDBEntities();

        // GET: AdminNarudzbina
        public ActionResult Index(int? currentPage)
        {
            int maxRows = 5;
            int pageNumber = (currentPage ?? 1);

            List<Narudzbina> narudzbinas = db.Narudzbinas.OrderByDescending(i => i.id_narudzbine).Skip((pageNumber - 1) * maxRows)
                .Take(maxRows).ToList();

            List<AdminNarudzbina> adminNarudzbinas = AdminNarudzbina.TransformList(narudzbinas);
            AdminNarudzbinaViewModel adminNarudzbinaViewModel = new AdminNarudzbinaViewModel
            {
                narudzbine = adminNarudzbinas
            };

            double pageCount = (double)((decimal)db.Knjiges.Count() / Convert.ToDecimal(maxRows));
            adminNarudzbinaViewModel.PageCount = (int)Math.Ceiling(pageCount);

            adminNarudzbinaViewModel.CurrentPageIndex = pageNumber;

            return View("~/Views/Back-end/AdminNarudzbina/Index.cshtml", adminNarudzbinaViewModel);
        }

        // GET: AdminNarudzbina/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Narudzbina adminNarudzbina = db.Narudzbinas.Find(id);
            if (adminNarudzbina == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Back-end/AdminNarudzbina/Edit.cshtml", adminNarudzbina);
        }

        // POST: AdminNarudzbina/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "status,id_narudzbine,datum,id_osoba")] Narudzbina narudzbina)
        {
            if (ModelState.IsValid)
            {
                db.Entry(narudzbina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("~/Views/Back-end/AdminNarudzbina/Edit.cshtml", narudzbina);
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