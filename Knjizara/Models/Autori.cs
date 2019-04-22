//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Knjizara.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class Autori
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Autori()
        {
            this.Knjiges = new HashSet<Knjige>();
        }
    
        public int id_autora { get; set; }

        [Remote("IsAuthorExist", "Autori", ErrorMessage = "Autor vec postoji.")]
        [MaxLength(50, ErrorMessage = "Naziv knjige moze imati maksimalno 50 karaktera.")]
        [RegularExpression(@"^[a-z����A-Z���Ǝ]+(\s[a-z����A-Z���Ǝ]+)?$", ErrorMessage = "Numericki karakteri nisu dozvoljeni.")]
        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        public string ime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Knjige> Knjiges { get; set; }
    }
}
