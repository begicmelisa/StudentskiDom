using Microsoft.AspNetCore.Mvc.Rendering;
using StudentskiDom.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentskiDom.Web.Areas.RecepcionerModul.ViewModels
{
    public class DodajRezervacijuVM
    {
		public List<Sala> sale { get; set; }
		public int Id { get; set; }
		public double UkupnaCijena { get; set; }
		public DateTime Datum { get; set; }
		public int BrojSati { get; set; }

		public List<SelectListItem> Zaposlenici { get; set; }
		public int? ZaposlnikID { get; set; }

		public List<SelectListItem> Posjetioci { get; set; }
		public int? PosjetilacID { get; set; }

		public List<SelectListItem> Dvorane { get; set; }
		public int? DvoranaID { get; set; }


	}
}
