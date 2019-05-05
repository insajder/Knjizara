using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Knjizara.Models
{
    public class Prijava
    {
        [DisplayName("Korisnicko ime:")]
        [Required(ErrorMessage = "Unesite korisnicko ime!")]
        public string KorisnickoImePrijava { get; set; }

        [DisplayName("Lozinka:")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Unesite lozinku!")]
        public string LozinkaPrijava { get; set; }

        public string LoginErrorMessage { get; set; }
    }
}