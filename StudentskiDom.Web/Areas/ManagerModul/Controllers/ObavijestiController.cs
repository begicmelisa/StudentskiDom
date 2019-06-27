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
    public class ObavijestiController : Controller
    {
        MojContext _context;

        public ObavijestiController(MojContext db)
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

            ObavijestiIndexVM model = new ObavijestiIndexVM
            {
                Rows = _context.Obavijesti.Select(x => new ObavijestiIndexVM.Row
                {
                    id = x.Id,
                    Datum = x.Datum,
                    Sadrzaj = x.Sadrzaj,
                    Naslov = x.Naslov,
                    Procitana = x.procitana,
                    zaSve = x.zaSve,
                    zaZaposlenike = x.samoZaposlenicima,
                    PostavioZaposlenik = x._Zaposlenik.Ime + " " + x._Zaposlenik.Prezime
                }).OrderByDescending(s => s.Datum).ToList()
            };
            return PartialView("Index", model);
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

            ObavijestiDodajVM model = new ObavijestiDodajVM
            {
                Datum = DateTime.Now
            };
            return View("Dodaj", model);
        }
        public IActionResult Snimi(ObavijestiDodajVM model)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Zaposlenik z = _context.Zaposlenici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || z == null || z._VrstaZaposlenikaId != 2)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            if (!ModelState.IsValid)
            {
                model.Datum = DateTime.Now;
               

            }
            Obavijesti novaObavijest = new Obavijesti
            {
                Datum = model.Datum,
                Sadrzaj = model.Sadrzaj,
                Naslov = model.Naslov,
                _ZaposlenikId = z.Id,
                zaSve = model.zaSve,
                samoZaposlenicima = !model.zaSve,
                procitana = false
            };

            _context.Obavijesti.Add(novaObavijest);
            _context.SaveChanges();


            return Redirect("/ManagerModul/Home/Index");
        }
        public IActionResult Obrisi(int id)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Zaposlenik z = _context.Zaposlenici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || z == null || z._VrstaZaposlenikaId != 2)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }

            Obavijesti ob = _context.Obavijesti.Where(x => x.Id == id).FirstOrDefault();
            _context.Obavijesti.Remove(ob);
            _context.SaveChanges();

            return Redirect("/ManagerModul/Home/Index");
        }
    }
}