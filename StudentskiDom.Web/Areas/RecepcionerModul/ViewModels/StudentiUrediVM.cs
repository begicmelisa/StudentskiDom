using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentskiDom.Web.Areas.RecepcionerModul.ViewModels
{
    public class StudentiUrediVM
    {

		public int Id { get; set; }
		public string Ime { get; set; }
		public string Prezime { get; set; }
		public string Spol { get; set; }
		public string Jmbg { get; set; }
		public string Email { get; set; }
		public string Adresa { get; set; }
		public string Telefon { get; set; }


		public string Soba_ { get; set; }
		
		public string DatumRodjenja { get; set; }
		public string DatumUpisa { get; set; }

		public List<SelectListItem> Fakulteti { get; set; }
		public int? FakultetId { get; set; }
		public List<SelectListItem> Gradovi { get; set; }
		public int? GradId { get; set; }

		public string Username { get; set; }
		public string Password { get; set; }
		
	}
}
