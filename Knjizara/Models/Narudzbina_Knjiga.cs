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
    
    public partial class Narudzbina_Knjiga
    {
        public int id_narudzbina_knjiga { get; set; }
        public int id_narudzbine { get; set; }
        public int id_knjige { get; set; }
        public int kolicina { get; set; }
    
        public virtual Knjige Knjige { get; set; }
        public virtual Narudzbina Narudzbina { get; set; }
    }
}
