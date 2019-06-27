using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentskiDom.Data.Models
{
	public class Sala
	{
		[Key]
		public int Id { get; set; }
		public string Naziv { get; set; }
		public int Kapacitet { get; set; }
		public double CijenaPoSatu { get; set; }

	}
}
