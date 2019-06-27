using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentskiDom.Data.Models
{
	public class Uplata
	{
		[Key]
		public int Id { get; set; }
		public double Iznos { get; set; }
		public DateTime Datum { get; set; }

		public Student _Student { get; set; }
		[ForeignKey(nameof(_Student))]
		public int _StudentId { get; set; }

		public Zaposlenik _Zaposlenik { get; set; }
		[ForeignKey(nameof(_Zaposlenik))]
		public int _ZaposlenikId { get; set; }

		public TipUplate _TipUplate { get; set; }
		[ForeignKey(nameof(_TipUplate))]
		public int _TipUplateId { get; set; }
	}
}
