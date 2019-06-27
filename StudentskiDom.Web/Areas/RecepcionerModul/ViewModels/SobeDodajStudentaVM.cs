using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentskiDom.Web.Areas.RecepcionerModul.ViewModels
{
    public class SobeDodajStudentaVM
    {
		public int SobaId { get; set; }
		public string Naziv { get; set; }

		public List<SelectListItem> Zaposlenici { get; set; }
		public int? ZaposlnikID { get; set; }

		public List<SelectListItem> Studenti { get; set; }
		public int? StudentID { get; set; }

		public string Napomena { get; set; }

	}
}
