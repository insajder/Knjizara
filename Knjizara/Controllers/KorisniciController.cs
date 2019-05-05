using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Knjizara.Models;

namespace Knjizara.Controllers
{
    public class KorisniciController : Controller
    {
        private KnjizaraDBEntities db = new KnjizaraDBEntities();

        // GET: Korisnici/Create
        public ActionResult Prijava()
        {
            return View("~/Views/Front-end/Korisnici/Prijava.cshtml");
        }

        // POST: Korisnici/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Prijava(Prijava korisnici)
        {
            var korisnikInfo = db.Korisnicis.Where(x => x.korisnicko_ime == korisnici.KorisnickoImePrijava && x.lozinka == korisnici.LozinkaPrijava).FirstOrDefault();
            if (korisnikInfo == null)
            {
                korisnici.LoginErrorMessage = "Pogresno korisnicko ime ili lozinka.";
                return View("~/Views/Front-end/Korisnici/Prijava.cshtml", korisnici);
            }
            else
            {
                Session["id_osoba"] = korisnikInfo.id_osoba;
                Session["korisnicko_ime"] = korisnikInfo.korisnicko_ime;
                Session["ime"] = korisnikInfo.ime;
                Session["prezime"] = korisnikInfo.prezime;
                Session["pol"] = korisnikInfo.pol;
                Session["telefon"] = korisnikInfo.telefon;
                Session["email"] = korisnikInfo.email;

                return RedirectToAction("Index", "Front");
            }
            
        }

        public ActionResult Odjava()
        {
            int id_osoba = (int)Session["id_osoba"];
            Session.Abandon();
            return RedirectToAction("Index", "Front");
        }


        // GET: Korisnici
        public ActionResult Index()
        {
            var korisnicis = db.Korisnicis.Include(k => k.VrstaKorisnika);
            return View("~/Views/Front-end/Korisnici/Index.cshtml", korisnicis.ToList());
        }

        // GET: Korisnici/Create
        public ActionResult Create()
        {
            ViewBag.id_vrsta_korisnika = new SelectList(db.VrstaKorisnikas, "id_vrsta_korisnika", "vrsta");
            return View("~/Views/Front-end/Korisnici/Create.cshtml");
        }

        // POST: Korisnici/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_osoba,ime,prezime,pol,telefon,id_vrsta_korisnika,email,korisnicko_ime,lozinka")] Korisnici korisnici)
        {
            if (ModelState.IsValid)
            {
                //korisnici.PotvrdjenaLozinka = korisnici.lozinka;

                db.Korisnicis.Add(korisnici);
                db.SaveChanges();
                return RedirectToAction("Prijava");
            }

            ViewBag.id_vrsta_korisnika = new SelectList(db.VrstaKorisnikas, "id_vrsta_korisnika", "vrsta", korisnici.id_vrsta_korisnika);
            return View("~/Views/Front-end/Korisnici/Create.cshtml", korisnici);
        }

        // GET: Korisnici/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Korisnici korisnici = db.Korisnicis.Find(id);
            if (korisnici == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_vrsta_korisnika = new SelectList(db.VrstaKorisnikas, "id_vrsta_korisnika", "vrsta", korisnici.id_vrsta_korisnika);
            return View("~/Views/Front-end/Korisnici/Edit.cshtml", korisnici);
        }

        // POST: Korisnici/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_osoba,ime,prezime,pol,telefon,id_vrsta_korisnika,email,korisnicko_ime,lozinka")] Korisnici korisnici)
        {
            if (ModelState.IsValid)
            {
                db.Entry(korisnici).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_vrsta_korisnika = new SelectList(db.VrstaKorisnikas, "id_vrsta_korisnika", "vrsta", korisnici.id_vrsta_korisnika);
            return View("~/Views/Front-end/Korisnici/Edit.cshtml", korisnici);
        }

        [HttpGet]
        public JsonResult IsUsernameExist(string korisnicko_ime)
        {
            Debug.WriteLine("USERNAME: " + korisnicko_ime);
            List<Korisnici> users = db.Korisnicis
                .Where(u => u.korisnicko_ime.Equals(korisnicko_ime)).ToList();

            Debug.WriteLine("LIST COUNT: " + users.Count);
            return Json(users.Count == 0, JsonRequestBehavior.AllowGet);
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
