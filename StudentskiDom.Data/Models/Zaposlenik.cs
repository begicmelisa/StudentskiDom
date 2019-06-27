using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentskiDom.Data.Models
{
	public class Zaposlenik
	{
		[Key]
		public int Id { get; set; }
		public string Ime { get; set; }
		public string Prezime { get; set; }
		public string JMBG { get; set; }
		public string Mail { get; set; }
		public string Telefon { get; set; }
		public Grad _Grad { get; set; }
		[ForeignKey(nameof(_Grad))]
		public int? _GradId { get; set; }
		public VrstaZaposlenika _VrstaZaposlenika { get; set; }
		[ForeignKey(nameof(_VrstaZaposlenika))]
		public int? _VrstaZaposlenikaId { get; set; }


		[ForeignKey(nameof(KorisnickiNalog))]
		public int? KorisnickiNalogId { get; set; }
		public KorisnickiNalog KorisnickiNalog { get; set; }
	}
}
