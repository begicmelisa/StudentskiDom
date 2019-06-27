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

    public class UplateController : Controller
    {

        private MojContext _context;

        public UplateController(MojContext db)
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
            UplateIndexVM model = new UplateIndexVM
            {
                Rows = _context.Uplate.Select(x => new UplateIndexVM.Row
                {
                    Datum = x.Datum,
                    Iznos = x.Iznos,
                    Student = _context.Studenti.Where(s => s.Id == x._StudentId).Select(s => s.Ime + " " + s.Prezime).FirstOrDefault().ToString(),
                    Zaposlenik = _context.Zaposlenici.Where(zaposlenik => zaposlenik.Id == x._ZaposlenikId).Select(zaposlenik => zaposlenik.Ime + " " + zaposlenik.Prezime).FirstOrDefault().ToString(),
                    TipUplate = _context.TipoviUplata.Where(uplata => uplata.Id == x._TipUplateId).Select(uplata => uplata.Naziv).FirstOrDefault().ToString(),

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
            UplateDodajVM model = new UplateDodajVM();
            model.Studenti = _context.Studenti.Select(a => new SelectListItem
            {
                Text = a.Ime + " " + a.Prezime,
                Value = a.Id.ToString()
            }).ToList();
            model.Zaposlenici = _context.Zaposlenici.Select(a => new SelectListItem
            {
                Text = a.Ime + " " + a.Prezime,
                Value = a.Id.ToString()
            }).ToList();
            model.TipoviUplata = _context.TipoviUplata.Select(a => new SelectListItem
            {
                Text = a.Naziv,
                Value = a.Id.ToString()
            }).ToList();
            model.Datum =DateTime.Now;

            return View(model);
        }
        public IActionResult Snimi(UplateDodajVM model)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Zaposlenik z = _context.Zaposlenici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || z == null || z._VrstaZaposlenikaId != 2)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            Uplata novaUplata = new Uplata
            {
                Datum = model.Datum,
                Iznos = model.Iznos,
                _StudentId = model.StudentId,
                _TipUplateId = model.TipoviUplataId,
                _ZaposlenikId = model.ZaposleniciId
            };
            _context.Uplate.Add(novaUplata);
            _context.SaveChanges();

            return Redirect("/ManagerModul/Uplate/Index");
        }
       public IActionResult Uredi(int id)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Zaposlenik z = _context.Zaposlenici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || z == null || z._VrstaZaposlenikaId != 2)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            UplateUrediVM model = _context.Uplate.Where(a => a.Id == id).Select(a => new UplateUrediVM
            {
                Datum=a.Datum,
                Iznos=a.Iznos,
                Studenti=_context.Studenti.Select(x=>new SelectListItem
                {
                    Text=x.Ime+" "+x.Prezime,
                    Value=x.Id.ToString()
                }).ToList(),
                Zaposlenici = _context.Zaposlenici.Select(za => new SelectListItem
                {
                    Text = za.Ime + " " + za.Prezime,
                    Value = za.Id.ToString()
                }).ToList(),
                TipUplate = _context.TipoviUplata.Select(p => new SelectListItem
                {
                    Text = p.Naziv,
                    Value = p.Id.ToString()
                }).ToList(),
            }).FirstOrDefault();
            return View(model);
        }
    }
}