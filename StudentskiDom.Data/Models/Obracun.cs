using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StudentskiDom.Data.Models
{
   public class Obracun
    {
        public int Id { get; set; }
        public int BrojNeradnihDana { get; set; }
        public int Ukupno { get; set; }

        public string Mjesec { get; set; }
        public string Godina { get; set; }
        
       public int Satnica { get; set; }
        public Zaposlenik _Zaposlenik { get; set; }
        [ForeignKey(nameof(_Zaposlenik))]
        public int _ZaposlenikId { get; set; }
       public int? VrstaZaposlenikaId { get; set; }


    }
}
