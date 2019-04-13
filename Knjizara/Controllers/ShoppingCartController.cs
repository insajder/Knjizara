using Knjizara.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Knjizara.Controllers
{
    public class ShoppingCartController : Controller
    {
        private KnjizaraDBEntities db = new KnjizaraDBEntities();
        private string strCart = "Cart";

        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View("~/Views/Front-end/ShoppingCart/Index.cshtml");
        }

        public ActionResult OrderNow(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if(Session[strCart] == null)
            {
                List<Cart> IsCart = new List<Cart>
                {
                    new Cart(db.Knjiges.Find(id), 1)
                };
                Session[strCart] = IsCart;
            }
            else
            {
                List<Cart> IsCart = (List<Cart>)Session[strCart];
                int check = IsExistingCheck(id);
                if(check == -1)
                {
                    IsCart.Add(new Cart(db.Knjiges.Find(id), 1));
                }
                else
                {
                    IsCart[check].Kolicina++;
                }
                Session[strCart] = IsCart;
            }
            return View("~/Views/Front-end/ShoppingCart/Index.cshtml");
        }

        private int IsExistingCheck(int? id)
        {
            List<Cart> IsCart = (List<Cart>)Session[strCart];
            for(int i = 0; i < IsCart.Count; i++)
            {
                if(IsCart[i].KnjigeKorpa.id_knjige == id)
                {
                    return i;
                }
            }
            return -1;
        }

        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int check = IsExistingCheck(id);
            List<Cart> IsCart = (List<Cart>)Session[strCart];
            IsCart.RemoveAt(check);
            return View("~/Views/Front-end/ShoppingCart/Index.cshtml");
        }
    }
}