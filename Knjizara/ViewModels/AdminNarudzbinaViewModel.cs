using Knjizara.Models;
using System.Collections.Generic;

namespace Knjizara.ViewModels
{
    public class AdminNarudzbinaViewModel
    {
        public List<AdminNarudzbina> narudzbine { get; set; }

        public int CurrentPageIndex { get; set; }

        public int PageCount { get; set; }
    }
}