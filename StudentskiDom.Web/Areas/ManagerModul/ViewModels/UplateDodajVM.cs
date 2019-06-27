using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentskiDom.Web.Areas.ManagerModul.ViewModels
{
    public class UplateDodajVM
    {
        public DateTime Datum { get; set; }
        public double Iznos { get; set; }
        public List<SelectListItem> Studenti { get; set; }
        public int StudentId { get; set; }
        public List<SelectListItem> TipoviUplata { get; set; }
        public int TipoviUplataId { get; set; }
        public List<SelectListItem> Zaposlenici { get; set; }
        public int ZaposleniciId { get; set; }
    }
}
