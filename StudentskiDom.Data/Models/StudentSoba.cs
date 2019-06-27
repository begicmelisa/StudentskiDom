using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentskiDom.Data.Models
{
	public class StudentSoba
	{
		[Key]
		public int Id { get; set; }
		public DateTime DatumDodjele { get; set; }
		public string Napomena { get; set; }

		
		public Soba _Soba { get; set; }
		[ForeignKey(nameof(_Soba))]
		public int _SobaId { get; set; }

		public Student _Student { get; set; }
		[ForeignKey(nameof(_Student))]
		public int _StudentId { get; set; }

		public Zaposlenik _Zaposlenik { get; set; }
		[ForeignKey(nameof(_Zaposlenik))]
		public int _ZaposlenikId { get; set; }
	}
}
