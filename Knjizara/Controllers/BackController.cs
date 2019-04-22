using Knjizara.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Knjizara.Controllers
{
    public class BackController : Controller
    {
        private KnjizaraDBEntities db = new KnjizaraDBEntities();

        // GET: Back
        public ActionResult Index()
        {
            ViewData["PorudzbinePoruka"] = db.Narudzbinas.Where(p => p.status == "Aktivna").Count();

            ViewData["SveKnjige"] = db.Knjiges.Count();
            int knjige = db.Knjiges.Count();

            ViewData["SviKorisnici"] = db.Korisnicis.Count();
            int korisnici = db.Korisnicis.Count();

            ViewData["UkupnoMuskaraca"] = db.Korisnicis.Where(o => o.pol == "Muski").Count();
            int muskarci = db.Korisnicis.Where(o => o.pol == "Muski").Count();

            ViewData["UkupnoZena"] = db.Korisnicis.Where(o => o.pol == "Zenski").Count();
            int zene = db.Korisnicis.Where(o => o.pol == "Zenski").Count();

            ViewData["SvePorudzbine"] = db.Narudzbinas.Count();
            int narudzbine = db.Narudzbinas.Count();

            List<DataPoint> dataPoints = new List<DataPoint>();

            dataPoints.Add(new DataPoint("Knjige", knjige));
            dataPoints.Add(new DataPoint("Korisnici", korisnici));
            dataPoints.Add(new DataPoint("Muskarci", muskarci));
            dataPoints.Add(new DataPoint("Zene", zene));
            dataPoints.Add(new DataPoint("Porudzbine", narudzbine));

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View("~/Views/Back-end/Index.cshtml");
        }
    }
}