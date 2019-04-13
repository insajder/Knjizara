using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Knjizara.Controllers
{
    public class KontaktController : Controller
    {
        // GET: Kontakt
        public ActionResult Index()
        {
            return View("~/Views/Front-end/Kontakt/Index.cshtml");
        }
    }
}