using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentskiDom.Web.Areas.ManagerModul.ViewModels
{
    public class ObavijestiIndexVM
    {
        public List<Row> Rows { get; set; }
        public class Row
        {
            public int id { get; set; }
            public DateTime Datum { get; set; }
            public string Naslov { get; set; }
            public string Sadrzaj { get; set; }
            public bool Procitana { get; set; }
            public bool zaSve { get; set; }
            public bool zaZaposlenike { get; set; }
            public string PostavioZaposlenik { get; internal set; }
        }
    }
}
