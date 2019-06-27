using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentskiDom.Data.Models
{
	public class Posjetilac
	{
		[Key]
		public int Id { get; set; }
		public string Ime { get; set; }
		public string Prezime { get; set; }
		public string JMBG { get; set; }
		public string Mail { get; set; }
		public string Telefon { get; set; }
	}
}
