using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentskiDom.Web.Areas.ManagerModul.ViewModels
{
    public class UplateIndexVM
    {
        public List<Row> Rows { get; set; }
        public class Row
        {
            public int Id { get; set; }
            public DateTime Datum { get; set; }
            public double Iznos { get; set; }
            public string Student { get; set; }
            public string TipUplate { get; set; }
            public string Zaposlenik { get; set; }

        }
    }
}
