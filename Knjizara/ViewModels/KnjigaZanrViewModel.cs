using Knjizara.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Knjizara.ViewModels
{
    public class KnjigaZanrViewModel
    {
        public List<Autori> autori { get; set; }
        public List<Zanr> zanrovi { get; set; }

        public int id_knjige { get; set; }

        [MaxLength(50, ErrorMessage = "Naziv knjige moze imati maksimalno 50 karaktera.")]
        [RegularExpression(@"^[a-zšđčćžA-ZŠĐČĆŽ]+(\s[a-zšđčćžA-ZŠĐČĆŽ]+)?$", ErrorMessage = "Numericki karakteri nisu dozvoljeni.")]
        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        public string naziv { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        public int id_autora { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        public string opis { get; set; }

        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Karakteri nisu dozvoljeni.")]
        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        public string cena { get; set; }
        
        public string slika { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        public List<int> odabraniZanrovi { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.jpeg)$", ErrorMessage = "Samo su slike dozvoljene.")]
        public HttpPostedFileBase PostedFile { get; set; }
    }
}