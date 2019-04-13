using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Knjizara.Models
{
    public class Cart
    {
        public Knjige KnjigeKorpa { get; set; }
        public int Kolicina { get; set; }

        public Cart (Knjige knjigeKorpa, int kolicina)
        {
            KnjigeKorpa = knjigeKorpa;
            Kolicina = kolicina;
        }
    }
}