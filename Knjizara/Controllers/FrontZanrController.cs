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
    public class FrontZanrController : Controller
    {
        private KnjizaraDBEntities db = new KnjizaraDBEntities();

        // GET: FrontZanr
        public ActionResult Index()
        {
            return View("~/Views/Front-end/layout/front_end_layout.cshtml", db.Zanrs.ToList().OrderBy(z => z.vrsta));
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
