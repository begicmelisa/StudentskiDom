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

    public class ZaposleniciController : Controller
    {
        MojContext _context;

        public ZaposleniciController(MojContext db)
        {
            _context = db;
        }
        public IActionResult Index(string searchString = null)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Zaposlenik z = _context.Zaposlenici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
            if (korisnik == null || z == null || z._VrstaZaposlenikaId != 2)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                ZaposleniciIndexVM modelS = new ZaposleniciIndexVM
                {
                    Rows = _context.Zaposlenici.Where(s => s.Ime.Contains(searchString) || s.Ime.Contains(searchString)).Select(x => new ZaposleniciIndexVM.Row
                    {
                        Id = x.Id,
                        Ime = x.Ime,
                        Prezime = x.Prezime,
                        Jmbg = x.JMBG,
                        Grad = x._Grad.Naziv,
                        Email = x.Mail,
                        KorisnickiNalog = x.KorisnickiNalog.KorisnickoIme,
                        VrstaZaposlenika = x._VrstaZaposlenika.Naziv

                    }).ToList()
                };

                return View(modelS);

            }

            else
            {
                ZaposleniciIndexVM model = new ZaposleniciIndexVM
                {
                    Rows = _context.Zaposlenici.Select(x => new ZaposleniciIndexVM.Row
                    {
                        Id = x.Id,
                        Ime = x.Ime,
                        Prezime = x.Prezime,
                        Jmbg = x.JMBG,
                        Grad = x._Grad.Naziv,
                        Email = x.Mail,
                        KorisnickiNalog = x.KorisnickiNalog.KorisnickoIme,
                        VrstaZaposlenika = x._VrstaZaposlenika.Naziv
                    }).ToList()
                };

                return View(model);
            }
        }
        public IActionResult Detalji(int id)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Zaposlenik z = _context.Zaposlenici.Where(x => x.Id == korisnik.Id).FirstOrDefault();
            if (korisnik == null || z == null || z._VrstaZaposlenikaId != 2)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            ZaposleniciDetaljiVM model = _context.Zaposlenici.Where(x => x.Id == id).Select(x => new ZaposleniciDetaljiVM
            {
                Id = x.Id,
                Ime = x.Ime,
                Prezime = x.Prezime,
                Jmbg = x.JMBG,
                Grad = x._Grad.Naziv,
                Email = x.Mail,
                Soba_ = _context.StudentiSobe.Where(s => s._StudentId == x.Id).FirstOrDefault()._Soba.Naziv,
                KorisnickoIme = _context.KorisnickiNalozi.Where(s=>s.Id==x.KorisnickiNalogId).FirstOrDefault().KorisnickoIme,
                Telefon = x.Telefon,
                VrstaZaposlenika = _context.VrsteZaposlenika.Where(v => v.Id == x.Id).FirstOrDefault().Naziv,

            }).FirstOrDefault();

            return View(model);
        }
        public IActionResult Dodaj()
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Zaposlenik z = _context.Zaposlenici.Where(x => x.Id == korisnik.Id).FirstOrDefault();
            if (korisnik == null || z == null || z._VrstaZaposlenikaId != 2)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            ZaposleniciDodajVM model = new ZaposleniciDodajVM();

            model.Gradovi = _context.Gradovi.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Naziv

            }).ToList();

            model.VrsteZaposlenika = _context.VrsteZaposlenika.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Naziv
            }).ToList();

            return View(model);
        }
        public IActionResult Snimi(ZaposleniciDodajVM model)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Zaposlenik z = _context.Zaposlenici.Where(x => x.Id == korisnik.Id).FirstOrDefault();
            if (korisnik == null || z == null || z._VrstaZaposlenikaId != 2)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }

            Zaposlenik noviZaposlenik = new Zaposlenik()
            {
                Ime = model.Ime,
                Prezime = model.Prezime,
                JMBG = model.Jmbg,
                Mail = model.Email,
                Telefon = model.Telefon,
                _GradId = model.GradId,
                _VrstaZaposlenikaId = model.VrsteZaposlenikaId
            };
            _context.Zaposlenici.Add(noviZaposlenik);
            _context.SaveChanges();
            KorisnickiNalog noviNalog = new KorisnickiNalog();
            noviNalog.KorisnickoIme = noviZaposlenik.Ime + "." + noviZaposlenik.Prezime;
            noviNalog.Lozinka = "0000";
            _context.KorisnickiNalozi.Add(noviNalog);
            _context.SaveChanges();

            noviZaposlenik.KorisnickiNalogId = noviNalog.Id;
            _context.Zaposlenici.Update(noviZaposlenik);
            _context.SaveChanges();

            return Redirect("/ManagerModul/Zaposlenici/Index");
        }
        public IActionResult Obrisi(int id)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Zaposlenik z = _context.Zaposlenici.Where(x => x.Id == korisnik.Id).FirstOrDefault();
            if (korisnik == null || z == null || z._VrstaZaposlenikaId != 2)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }

            Zaposlenik zaposlenik = _context.Zaposlenici.Where(x => x.Id == id).FirstOrDefault();
            KorisnickiNalog kn = _context.KorisnickiNalozi.Where(x => x.KorisnickoIme == zaposlenik.Ime + "." + zaposlenik.Prezime).FirstOrDefault();
            if (kn != null)
            {
                _context.KorisnickiNalozi.Remove(kn);
                _context.SaveChanges();
            }
            StudentSoba ss = _context.StudentiSobe.Where(s => s._ZaposlenikId == id).FirstOrDefault();
            if (ss != null)
            {
                _context.StudentiSobe.Remove(ss);
                _context.SaveChanges();
            }
            _context.Zaposlenici.Remove(zaposlenik);
            _context.SaveChanges();
            return Redirect("/ManagerModul/Zaposlenici/Index");
        }
        public IActionResult Edit(int id)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Zaposlenik z = _context.Zaposlenici.Where(x => x.Id == korisnik.Id).FirstOrDefault();
            if (korisnik == null || z == null || z._VrstaZaposlenikaId != 2)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }
            ZaposleniciEditVM model = _context.Zaposlenici.Where(x => x.Id == id).Select(x => new ZaposleniciEditVM
            {
                Id = x.Id,
                Ime = x.Ime,
                Prezime = x.Prezime,
                Jmbg = x.JMBG,
                Telefon = x.Telefon,
                Email = x.Mail,
                Password = _context.KorisnickiNalozi.Where(s => s.KorisnickoIme == x.Ime + "." + x.Prezime).FirstOrDefault().Lozinka,
                Username = _context.KorisnickiNalozi.Where(s => s.KorisnickoIme == x.Ime + "." + x.Prezime).FirstOrDefault().KorisnickoIme,
            }).FirstOrDefault();

            model.VrsteZaposlenika = _context.VrsteZaposlenika.Select(a => new SelectListItem
            {
                Text=a.Naziv,
                Value=a.Id.ToString()
            }).ToList();

            model.Gradovi = _context.Gradovi.Select(a => new SelectListItem
            {
                Text = a.Naziv,
                Value = a.Id.ToString()
            }).ToList();

            return View(model);
        }
        public IActionResult SnimiPromjene(ZaposleniciEditVM model)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            Zaposlenik z = _context.Zaposlenici.Where(x => x.Id == korisnik.Id).FirstOrDefault();
            if (korisnik == null || z == null || z._VrstaZaposlenikaId != 2)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return Redirect("/Autentifikacija/Index");
            }

            Zaposlenik zaposlenik = _context.Zaposlenici.Where(x => x.Id == model.Id).FirstOrDefault();


            zaposlenik.Ime = model.Ime;
            zaposlenik.Prezime = model.Prezime;
            zaposlenik.JMBG = model.Jmbg;
            zaposlenik._GradId = model.GradId;
            zaposlenik.Mail = model.Email;
            zaposlenik.Telefon = model.Telefon;
            zaposlenik._VrstaZaposlenikaId = model.VrsteZaposlenikaId;

            KorisnickiNalog kn = _context.KorisnickiNalozi.Where(w => w.KorisnickoIme == zaposlenik.Ime + "." + zaposlenik.Prezime).FirstOrDefault();
            if (kn == null)
            {
                kn = new KorisnickiNalog
                {
                    KorisnickoIme = model.Username,
                    Lozinka = model.Password
                };
            }
            else
            {
                kn.KorisnickoIme = model.Username;
                kn.Lozinka = model.Password;
            }
            _context.Zaposlenici.Update(zaposlenik);
            _context.KorisnickiNalozi.Update(kn);
            _context.SaveChanges();

            return Redirect("/ManagerModul/Zaposlenici/Index");
        }
    }
}