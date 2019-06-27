using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentskiDom.Data.Models
{
	public class Fakultet
	{
		[Key]
		public int Id { get; set; }
		public string Naziv { get; set; }
		public string Adresa { get; set; }
		public string Mail { get; set; }


	}
}
