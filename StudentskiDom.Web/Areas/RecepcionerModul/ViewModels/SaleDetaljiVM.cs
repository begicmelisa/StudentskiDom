using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentskiDom.Web.Areas.RecepcionerModul.ViewModels
{
    public class SaleDetaljiVM
    {
		public List<Row> Rows { get; set; }
		public class Row
		{
			public int Id { get; set; }
			public string Naziv { get; set; }
			public int Kapacitet { get; set; }
			public double CijenaPoSatu { get; set; }
		}
	}
}
