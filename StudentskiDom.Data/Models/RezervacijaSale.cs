using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentskiDom.Data.Models
{
	public class RezervacijaSale
	{
		[Key]
		public int Id { get; set; }
		public double UkupnaCijena { get; set; }
		public DateTime Datum { get; set; }
		public int BrojSati { get; set; }

		public Zaposlenik _Zaposlenik { get; set; }
		[ForeignKey(nameof(_Zaposlenik))]
		public int _ZaposlenikId { get; set; }

		public Posjetilac _Posjetilac { get; set; }
		[ForeignKey(nameof(_Posjetilac))]
		public int _PosjetilacId { get; set; }

		public Sala _Sala { get; set; }
		[ForeignKey(nameof(_Sala))]
		public int _SalaId { get; set; }
	}
}
