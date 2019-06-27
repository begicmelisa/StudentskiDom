using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentskiDom.Web.Areas.ManagerModul.ViewModels
{
    public class ObracunDodajVM
    {
        public int BrojNeradnihDana { get; set; }
        public int BrojPrekovremenihSati { get; set; }

        public List<SelectListItem> Zaposlenici { get; set; }
        public int ZaposleniciId { get; set; }

    }
}
