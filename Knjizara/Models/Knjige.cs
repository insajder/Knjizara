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

    public partial class Knjige
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Knjige()
        {
            this.Knjige_Zanr = new HashSet<Knjige_Zanr>();
            this.Narudzbina_Knjiga = new HashSet<Narudzbina_Knjiga>();
            this.Omiljenoes = new HashSet<Omiljeno>();
        }
    
        public int id_knjige { get; set; }
        public string naziv { get; set; }
        public int id_autora { get; set; }
        public string slika { get; set; }
        public string opis { get; set; }
        public int cena { get; set; }
    
        public virtual Autori Autori { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Knjige_Zanr> Knjige_Zanr { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Narudzbina_Knjiga> Narudzbina_Knjiga { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Omiljeno> Omiljenoes { get; set; }
    }
}
