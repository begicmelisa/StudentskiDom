using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentskiDom.Web.Areas.ManagerModul.ViewModels
{
    public class ZaposleniciIndexVM
    {
        public List<Row> Rows { get; set; }
        public class Row
        {
            public int Id { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string Jmbg { get; set; }
            public string Email { get; set; }
            public string Grad { get; set; }
            public string VrstaZaposlenika { get; set; }
            public string KorisnickiNalog { get; set; }
        }
    }
}
