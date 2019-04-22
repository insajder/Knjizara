using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Knjizara.Models;
using Knjizara.ViewModels;
using PagedList;

namespace Knjizara.Controllers
{
    public class FrontKnjigeController : Controller
    {
        private KnjizaraDBEntities db = new KnjizaraDBEntities();

        // GET: Knjige
        public ActionResult Index(string odabraniZanrString, int? currentPage, string search)
        {
            int maxRows = 5;
            int pageNumber = (currentPage ?? 1);

            KnjigaZanroviViewModel knjigaZanroviViewModel = new KnjigaZanroviViewModel();
            List<KnjigaSaZanrovima> knjigeSaZanrovima = new List<KnjigaSaZanrovima>();

            Zanr odabraniZanr = null;
            if (odabraniZanrString != null)
            {
                odabraniZanr = db.Zanrs.Where(z => z.vrsta == odabraniZanrString).First();
            }

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
            }
            else
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

            if (odabraniZanr != null)
            {
                knjigeSaZanrovima = KnjigaSaZanrovima.TransformList(knjiges.OrderByDescending(i => i.id_knjige).ToList(), odabraniZanr);
            } else
            {
                knjigeSaZanrovima = KnjigaSaZanrovima.TransformList(knjiges.OrderByDescending(i => i.id_knjige).ToList());
            }

            knjigaZanroviViewModel.KnjigeSaZanrovima = knjigeSaZanrovima;
            knjigaZanroviViewModel.CurrentPageIndex = pageNumber;

            return View("~/Views/Front-end/FrontKnjige/Index.cshtml", knjigaZanroviViewModel);
        }

        public ActionResult Detaljnije(int? id)
        {
            Knjige knjiga = db.Knjiges
                .Where(k => k.id_knjige == id)
                .Include(k => k.Autori)
                .First();
            if (knjiga == null)
            {
                return HttpNotFound();
            }

            KnjigaSaZanrovima knjigaSaZanrovima = KnjigaSaZanrovima.TransformSingle(knjiga);
            return View("~/Views/Front-end/FrontKnjige/Detaljnije.cshtml", knjigaSaZanrovima);
        }

        public ActionResult Omiljeno()
        {
            var trenutniKorisnikId = int.Parse(Session["id_osoba"].ToString());

            List<Omiljeno> sveOmiljenoZaKorisnika = db.Omiljenoes
                .Where(o => o.id_osoba == trenutniKorisnikId)
                .ToList();

            List<KnjigaSaZanrovima> knjigeOmiljene = new List<KnjigaSaZanrovima>();

            foreach (Omiljeno o in sveOmiljenoZaKorisnika)
            {
                Knjige oKnjiga = db.Knjiges
                    .Where(knj => knj.id_knjige == o.id_knjige)
                    .Include(knj => knj.Autori)
                    .ToList()
                    .First();
                KnjigaSaZanrovima knjigaSaZanrovima = KnjigaSaZanrovima.TransformSingle(oKnjiga);
                knjigeOmiljene.Add(knjigaSaZanrovima);
            }

            KnjigaZanroviViewModel knjigaZanroviViewModel = new KnjigaZanroviViewModel
            {
                KnjigeSaZanrovima = knjigeOmiljene
            };

            return View("~/Views/Front-end/FrontKnjige/Omiljeno.cshtml", knjigaZanroviViewModel);
        }

        public ActionResult DodajOmiljeno(int? id)
        {
            var trenutniKorisnikId = int.Parse(Session["id_osoba"].ToString());
            Knjige knjiga = db.Knjiges
                .Where(k => k.id_knjige == id)
                .Include(k => k.Autori)
                .First();
            if (knjiga == null)
            {
                return HttpNotFound();
            }

            Omiljeno omiljenaKnjiga = new Omiljeno
            {
                id_osoba = trenutniKorisnikId,
                id_knjige = knjiga.id_knjige
            };

            List<Omiljeno> provera = db.Omiljenoes
                .Where(o => o.id_knjige == id && o.id_osoba == trenutniKorisnikId)
                .ToList();
            if (provera == null || provera.Count == 0)
            {
                db.Omiljenoes.Add(omiljenaKnjiga);
                db.SaveChanges();
            }

            return RedirectToAction("Omiljeno", "FrontKnjige");
        }
        
        public ActionResult Delete(int id)
        {
            var trenutniKorisnikId = int.Parse(Session["id_osoba"].ToString());

            List<Omiljeno> omiljeno = db.Omiljenoes
                 .Where(o => o.id_knjige == id && o.id_osoba == trenutniKorisnikId)
                 .ToList();

            db.Omiljenoes.RemoveRange(omiljeno);
            db.SaveChanges();

            return RedirectToAction("Omiljeno");
        }

        public ActionResult KreirajNarudzbinu(string Narudzbenice)
        {
            // Narudzbenice --> "1:1,7:1,3:2"
            var trenutniKorisnikId = int.Parse(Session["id_osoba"].ToString());

            Narudzbina narudzbina = new Narudzbina
            {
                id_osoba = trenutniKorisnikId,
                datum = DateTime.Now,
                status = AdminNarudzbina.AKTIVNA
            };

            narudzbina = db.Narudzbinas.Add(narudzbina);
            db.SaveChanges();

            List<string> narudzbenicePairsString = Narudzbenice.Split(',').ToList(); // --> [ "1:1", "7:1", "3:2" ]
            Debug.WriteLine("COUNT: " + narudzbenicePairsString.Count());

            List<Narudzbina_Knjiga> narudzbina_Knjigas = new List<Narudzbina_Knjiga>();
            foreach(var stringPair in narudzbenicePairsString)
            {
                List<string> narudzbeniceKeyVal = stringPair.Split(':').ToList(); // --> [ "1", "1" ]

                int IDKnjige = int.Parse(narudzbeniceKeyVal.ElementAt(0)); // --> 1
                int Kolicina = int.Parse(narudzbeniceKeyVal.ElementAt(1)); // --> 1
                Narudzbina_Knjiga nk = new Narudzbina_Knjiga
                {
                    id_knjige = IDKnjige,
                    kolicina = Kolicina,
                    id_narudzbine = narudzbina.id_narudzbine
                };
                narudzbina_Knjigas.Add(nk);
            }
            db.Narudzbina_Knjiga.AddRange(narudzbina_Knjigas);
            db.SaveChanges();
            Session["Cart"] = null;

            return View("~/Views/Front-end/FrontKnjige/Narudzbina.cshtml");
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
