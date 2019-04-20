using Knjizara.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Knjizara.ViewModels
{
    public class KnjigaZanroviViewModel
    {
        public List<KnjigaSaZanrovima> KnjigeSaZanrovima { get; set; }

        public int CurrentPageIndex { get; set; }

        public int PageCount { get; set; }
    }
}
