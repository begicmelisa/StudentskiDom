using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentskiDom.Data.Models
{
	public class Soba
	{
		[Key]
		public int Id { get; set; }
		public string Naziv { get; set; }
		public int BrojKreveta { get; set; }
		public int Sprat { get; set; }
		public TipSobe _TipSobe { get; set; }
		[ForeignKey(nameof(_TipSobe))]
		public int? _TipSobeId { get; set; }

	}
}
