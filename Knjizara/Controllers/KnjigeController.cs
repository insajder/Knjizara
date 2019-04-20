using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Knjizara.Models;
using Knjizara.ViewModels;

namespace Knjizara.Controllers
{
    public class KnjigeController : Controller
    {
        private KnjizaraDBEntities db = new KnjizaraDBEntities();

        // GET: Knjige
        public ActionResult Index(int? currentPage, string search)
        {
            int maxRows = 5;
            int pageNumber = (currentPage ?? 1);

            KnjigaZanroviViewModel knjigaZanroviViewModel = new KnjigaZanroviViewModel();
            List <KnjigaSaZanrovima> knjigeSaZanrovima = new List<KnjigaSaZanrovima>();

            List<Knjige> knjiges;
            if (!String.IsNullOrEmpty(search))
            {
                knjiges = db.Knjiges
                .Include(k => k.Autori)
                .Where(k => k.naziv.Contains(search) || k.Autori.ime.Contains(search))
                .OrderByDescending(i => i.id_knjige)
                .Skip((pageNumber - 1) * maxRows)
                .Take(maxRows)
                .ToList();
            } else
            {
                knjiges = db.Knjiges
                .Include(k => k.Autori)
                .OrderByDescending(i => i.id_knjige)
                .Skip((pageNumber - 1) * maxRows)
                .Take(maxRows)
                .ToList();
            }

            double pageCount = (double)((decimal)db.Knjiges.Count() / Convert.ToDecimal(maxRows));
            knjigaZanroviViewModel.PageCount = (int)Math.Ceiling(pageCount);

            foreach (Knjige knjiga in knjiges)
            {
                List<Knjige_Zanr> knjigeZanrovi = db.Knjige_Zanr
                    .Where(kz => kz.id_knjige == knjiga.id_knjige)
                    .ToList();

                List<Zanr> zanrovi = new List<Zanr>();
                foreach (Knjige_Zanr kz in knjigeZanrovi)
                {
                    Zanr zanr = db.Zanrs.Find(kz.id_zanr);
                    zanrovi.Add(zanr);
                }

                KnjigaSaZanrovima knjigaSaZanrovima = new KnjigaSaZanrovima
                {
                    knjiga = knjiga,
                    zanrovi = zanrovi
                };

                knjigeSaZanrovima.Add(knjigaSaZanrovima);
            }

            knjigaZanroviViewModel.KnjigeSaZanrovima = knjigeSaZanrovima;
            knjigaZanroviViewModel.CurrentPageIndex = pageNumber;

            return View("~/Views/Back-end/Knjige/Index.cshtml", knjigaZanroviViewModel);
        }

        // GET: Knjige/Create
        public ActionResult Create()
        {
            KnjigaZanrViewModel knjigaZanrViewModel = new KnjigaZanrViewModel
            {
                autori = db.Autoris.OrderBy(a => a.ime).ToList(),
                zanrovi = db.Zanrs.OrderBy(z => z.vrsta).ToList()
            };

            return View("~/Views/Back-end/Knjige/Create.cshtml", knjigaZanrViewModel);
        }

        // POST: Knjige/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "id_knjige,naziv,id_autora,slika,opis,cena")] Knjige knjige,
            [Bind(Include = "odabraniZanrovi")] List<int> odabraniZanrovi,
            HttpPostedFileBase postedFile
        ) {
            if (ModelState.IsValid)
            {
                if (postedFile != null)
                {
                    string nazivSlike = System.IO.Path.GetFileName(postedFile.FileName);
                    string putanja = Server.MapPath("~/Images/" + nazivSlike);
                    postedFile.SaveAs(putanja);
                    knjige.slika = nazivSlike;

                    ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", nazivSlike);
                }

                Knjige novaKnjiga = db.Knjiges.Add(knjige);

                foreach (int zanr in odabraniZanrovi)
                {
                    Knjige_Zanr kz = new Knjige_Zanr
                    {
                        id_knjige = novaKnjiga.id_knjige,
                        id_zanr = zanr
                    };
                    db.Knjige_Zanr.Add(kz);
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.id_autora = new SelectList(db.Autoris, "id_autora", "ime", knjige.id_autora);
            return View("~/Views/Back-end/Knjige/Create.cshtml", knjige);
        }

        // GET: Knjige/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Knjige knjige = db.Knjiges.Find(id);
            if (knjige == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_autora = new SelectList(db.Autoris, "id_autora", "ime", knjige.id_autora);
            return View("~/Views/Back-end/Knjige/Edit.cshtml", knjige);
        }

        // POST: Knjige/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_knjige,naziv,id_autora,slika,opis,cena")] Knjige knjige)
        {
            if (ModelState.IsValid)
            {
                db.Entry(knjige).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_autora = new SelectList(db.Autoris, "id_autora", "ime", knjige.id_autora);
            return View("~/Views/Back-end/Knjige/Edit.cshtml", knjige);
        }

        // GET: Knjige/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Knjige knjige = db.Knjiges.Find(id);
            if (knjige == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Back-end/Knjige/Delete.cshtml", knjige);
        }

        // POST: Knjige/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Knjige knjige = db.Knjiges.Find(id);
            List<Knjige_Zanr> knjige_Zanrs = db.Knjige_Zanr.Where(kz => kz.id_knjige == knjige.id_knjige).ToList();
            db.Knjige_Zanr.RemoveRange(knjige_Zanrs);
            db.SaveChanges();
            db.Knjiges.Remove(knjige);
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
