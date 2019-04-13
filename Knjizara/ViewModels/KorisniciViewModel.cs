using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Knjizara.ViewModels
{
    public class KorisniciViewModel
    {
        public int id_osoba { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public Pol pol { get; set; }
        public string telefon { get; set; }
        public Nullable<int> id_vrsta_korisnika { get; set; }
        public string email { get; set; }
        public string korisnicko_ime { get; set; }
        public string lozinka { get; set; }

    }

    public enum Pol
    {
        M,
        Ž
    }
}