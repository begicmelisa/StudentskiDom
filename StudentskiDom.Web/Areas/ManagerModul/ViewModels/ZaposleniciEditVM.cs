using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentskiDom.Web.Areas.ManagerModul.ViewModels
{
    public class ZaposleniciEditVM
    {

        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Jmbg { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }


        public List<SelectListItem> VrsteZaposlenika { get; set; }
        public int? VrsteZaposlenikaId { get; set; }
        public List<SelectListItem> Gradovi { get; set; }
        public int? GradId { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

    }
}
