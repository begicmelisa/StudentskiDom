using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentskiDom.Web.Areas.RecepcionerModul.ViewModels
{
    public class RezervacijeIndexVM
    {
		public List<Row> Rows { get; set; }
		public class Row
		{
			public int Id { get; set; }
			public double UkupnaCijena { get; set; }
			public DateTime Datum { get; set; }
			public int BrojSati { get; set; }
			public string zaposlenik { get; set; }
			public string posjetilac { get; set; }
			public string sala { get; set; }
		}
	}
}
