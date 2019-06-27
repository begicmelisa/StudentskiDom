using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentskiDom.Data.EF;
using StudentskiDom.Data.Models;
using StudentskiDom.Web.Areas.ManagerModul.ViewModels;
using StudentskiDom.Web.Helper;

namespace StudentskiDom.Web.Areas.ManagerModul.Controllers
{
    [Area("ManagerModul")]

    public class ObracunController : Controller
    {
        MojContext _context;

        public ObracunController(MojContext db)
        {
            _context = db;
        }
        public IActionResult Index()
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Zaposlenik z = _context.Zaposlenici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || z == null || z._VrstaZaposlenikaId != 2)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
           
                ObracunIndexVM model = new ObracunIndexVM
                {
                    Rows = _context.Obracun.Select(x => new ObracunIndexVM.Row
                    {
                     BrojNeradnihDana=x.BrojNeradnihDana,
                     Godina=DateTime.Now.Year.ToString(),
                     Mjesec=DateTime.Now.Month.ToString(),
                     Ukupno=x.Ukupno,
                     Zaposlenik=x._Zaposlenik.Ime+" "+x._Zaposlenik.Prezime   ,
                     Satnica=x.Satnica
                    }).ToList()
                };              
            return View(model);
        }
        public IActionResult Dodaj()
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Zaposlenik z = _context.Zaposlenici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || z == null || z._VrstaZaposlenikaId != 2)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            ObracunDodajVM model = new ObracunDodajVM();

            model.Zaposlenici = _context.Zaposlenici.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Ime+" "+x.Prezime

            }).ToList();
            return View(model);
        }
        public IActionResult Snimi(ObracunDodajVM model)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Zaposlenik z = _context.Zaposlenici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || z == null || z._VrstaZaposlenikaId != 2)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }

            Obracun novi = new Obracun()
            {
                BrojNeradnihDana=model.BrojNeradnihDana,
                Godina="",
                Mjesec="",
                Ukupno=(22-model.BrojNeradnihDana)*20,
                _ZaposlenikId=model.ZaposleniciId,
                Satnica=0,
                VrstaZaposlenikaId=0,
                
                
            };
            _context.Obracun.Add(novi);
            _context.SaveChanges();
            novi.VrstaZaposlenikaId = _context.Zaposlenici.Where(a => a.Id == novi._ZaposlenikId).Select(a => a._VrstaZaposlenikaId as int?).FirstOrDefault();
            novi.Satnica = _context.VrsteZaposlenika.Where(k => k.Id == novi.VrstaZaposlenikaId).Select(k => k.IznosSatnice).FirstOrDefault();
            novi.Ukupno = ((22 - novi.BrojNeradnihDana) * novi.Satnica * 8)+(model.BrojPrekovremenihSati*(novi.Satnica+2));
            _context.Obracun.Update(novi);
            _context.SaveChanges();
            return Redirect("/ManagerModul/Obracun/Index");


        }
    }
}