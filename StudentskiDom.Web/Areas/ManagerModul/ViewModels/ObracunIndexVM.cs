using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentskiDom.Web.Areas.ManagerModul.ViewModels
{
    public class ObracunIndexVM
    {
        public List<Row> Rows { get; set; }
        public class Row
        {
            public int Id { get; set; }
            public int BrojNeradnihDana { get; set; }
            public int Ukupno { get; set; }

            public string Mjesec { get; set; }
            public string Godina { get; set; }
            public string Zaposlenik { get; set; }
            public int Satnica { get; set; }

        }
    }
}
