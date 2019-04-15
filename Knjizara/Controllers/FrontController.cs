using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Knjizara.Models;

namespace Knjizara.Controllers
{
    public class FrontController : Controller
    {
        private KnjizaraDBEntities db = new KnjizaraDBEntities();

        private static string zanroviMeni = "zanr_menu";

        // GET: Front
        public ActionResult Index()
        {
            List<Zanr> zanrovi = db.Zanrs.OrderBy(z => z.vrsta).ToList();
            Session[zanroviMeni] = zanrovi;

            return View("~/Views/Front-end/Index.cshtml");
        }
    }
}