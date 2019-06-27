using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Threading.Tasks;

namespace StudentskiDom.Data.Models
{
	public class Student
	{
		[Key]
		public int Id { get; set; }
		public string Ime { get; set; }
		public string Prezime { get; set; }
		public string JMBG { get; set; }
		public string Mail { get; set; }
		public string Telefon { get; set; }
		public string Adresa { get; set; }
		public string Spol { get; set; }
		public DateTime DatumRodjenja { get; set; }
		public Fakultet _Fakultet { get; set; }
		[ForeignKey(nameof(_Fakultet))]
		public int? _FakultetId { get; set; }
		public Grad _Grad { get; set; }
		[ForeignKey(nameof(_Grad))]
		public int? _GradId { get; set; }

		[ForeignKey(nameof(KorisnickiNalog))]
		public int? KorisnickiNalogId { get; set; }
		public KorisnickiNalog KorisnickiNalog { get; set; }

	
    }
}
