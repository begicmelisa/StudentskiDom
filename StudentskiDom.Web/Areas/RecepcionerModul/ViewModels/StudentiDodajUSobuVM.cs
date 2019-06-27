using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentskiDom.Web.Areas.RecepcionerModul.ViewModels
{
    public class StudentiDodajUSobuVM
    {
		public int StudentId { get; set; }
		public string ImeiPrezime { get; set; }
		public List<SelectListItem> SlobodneSobe { get; set; }
		public int? SlobodnaSobaId { get; set; }

		public List<SelectListItem> Zaposlenici { get; set; }
		public int? ZaposlnikID { get; set; }
	}
}
