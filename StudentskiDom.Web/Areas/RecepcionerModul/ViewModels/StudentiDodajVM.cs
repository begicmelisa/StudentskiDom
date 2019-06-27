using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentskiDom.Web.Areas.RecepcionerModul.ViewModels
{
    public class StudentiDodajVM
    {
		[Required(ErrorMessage = "Obavezno polje")]
		public string Ime { get; set; }
		[Required(ErrorMessage = "Obavezno polje")]
		public string Prezime { get; set; }
		public string Spol { get; set; }
		[Required(ErrorMessage = "Datum rodjenja je obavezan!")]
		[DisplayFormat(DataFormatString = "dd.MM.yyyy")]
		public DateTime? DatumRodjenja { get; set; }
		public string Jmbg { get; set; }
		public string Email { get; set; }
		public string Telefon { get; set; }

		public string Adresa { get; set; }
		public List<SelectListItem> Fakulteti { get; set; }
		public int? FakultetId { get; set; }
		public List<SelectListItem> Gradovi { get; set; }
		public int? GradId { get; set; }
		public List<SelectListItem> SlobodneSobe { get; set; }
		public int? SlobodnaSobaId { get; set; }


	}
}
