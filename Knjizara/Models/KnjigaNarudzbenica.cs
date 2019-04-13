using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Knjizara.Models
{
    public class KnjigaNarudzbenica
    {
        public int IDKnjige { get; set; }
        public string Naziv { get; set; }
        public int Cena { get; set; }
        public int Kolicina { get; set; }
    }
}