using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Knjizara.Models
{
    public class Kontakt
    {
        [RegularExpression(@"^[a-zšđčćžA-ZŠĐČĆŽ]+(\s[a-zšđčćžA-ZŠĐČĆŽ]+)?$", ErrorMessage = "Numericki karakteri nisu dozvoljeni.")]
        [Required(ErrorMessage = "Unesite ime!")]
        [MaxLength(40, ErrorMessage = "Ime moze imati maksimalno 40 karaktera.")]
        public string Ime { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "Email adresa nije validna.")]
        [Required(ErrorMessage = "Unesite email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Unesite naslov!")]
        public string Naslov { get; set; }

        [Required(ErrorMessage = "Unesite poruku!")]
        [MaxLength(200, ErrorMessage = "Poruka moze imati maksimalno 200 karaktera.")]
        public string Poruka { get; set; }
    }
}