using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentskiDom.Web.Areas.ManagerModul.ViewModels
{
    public class UplateUrediVM
    {
        public int Id { get; set; }
        public double Iznos { get; set; }
        public DateTime Datum { get; set; }
        public List<SelectListItem> Studenti { get; set; }
        public int StudentiId { get; set; }
        public List<SelectListItem> Zaposlenici { get; set; }
        public int ZaposleniciId { get; set; }
        public List<SelectListItem> TipUplate { get; set; }
        public int TipUplateId { get; set; }
    }
}
