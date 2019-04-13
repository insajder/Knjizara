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
    public class ListaZanrController : Controller
    {
        private KnjizaraDBEntities db = new KnjizaraDBEntities();

        // GET: Zanr
        public ActionResult Index()
        {
            return View("~/Views/Front-end/layout/NavBar.cshtml", db.Zanrs.ToList().OrderBy(z => z.vrsta));
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
