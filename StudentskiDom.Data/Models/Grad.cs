using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentskiDom.Data.Models
{
	public class Grad
	{
		[Key]
		public int Id { get; set; }
		public string Naziv { get; set; }
		public Drzava _Drzava { get; set; }
		[ForeignKey(nameof(_Drzava))]
		public int _DrzavaId { get; set; }
	}
}
