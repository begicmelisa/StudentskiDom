using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StudentskiDom.Data.Models
{
    public class Obavijesti
    {
		[Key]
		public int Id { get; set; }
		public string Naslov { get; set; }
		public string Sadrzaj { get; set; }
		public DateTime Datum { get; set; }
		public bool procitana { get; set; }
		public bool zaSve { get; set; }
		public bool samoZaposlenicima { get; set; }
		public Zaposlenik _Zaposlenik { get; set; } //ko je postavio
		[ForeignKey(nameof(_Zaposlenik))]
		public int _ZaposlenikId { get; set; }


	}
}
