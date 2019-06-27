using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentskiDom.Web.Areas.ManagerModul.ViewModels
{
    public class ObavijestiDodajVM
    {
        public DateTime Datum { get; set; }
        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }
        public bool Procitana { get; set; }
        public bool zaSve { get; set; }
        public bool zaZaposlenike { get; set; }
      
    }
}
