using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentskiDom.Web.Areas.StudentModul.ViewModels
{
    public class UplateIndexVM
    {
		public List<Row> Rows { get; set; }
		public List<string> Mjeseci { get; set; }
		public class Row {
			public int Id { get; set; }
			public DateTime Datum { get; set; }
			public double Iznos { get; set; }
			public string TipUplate { get; set; }
			public int StudentID { get; set; }
			public string zaposlenik { get; set; }
			public int Month { get; set; }
			

		}
	}
}
