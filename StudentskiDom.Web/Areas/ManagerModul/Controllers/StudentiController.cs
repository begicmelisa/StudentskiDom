using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentskiDom.Data.EF;
using StudentskiDom.Data.Models;
using StudentskiDom.Web.Areas.ManagerModul.ViewModels;
using StudentskiDom.Web.Helper;

namespace StudentskiDom.Web.Areas.ManagerModul.Controllers
{
    [Area("ManagerModul")]
    public class StudentiController : Controller
    {
        MojContext _context;

        public StudentiController(MojContext db)
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
                StudentiIndexVM modelS = new StudentiIndexVM
                {
                    Rows = _context.Studenti.Where(s => s.Ime.Contains(searchString) || s.Ime.Contains(searchString)).Select(x => new StudentiIndexVM.Row
                    {
                        Id = x.Id,
                        Ime = x.Ime,
                        Prezime = x.Prezime,
                        Spol = x.Spol,
                        Jmbg = x.JMBG,
                        Grad = x._Grad.Naziv,
                        Email = x.Mail,

                    }).ToList()
                };
                return View("Index", modelS);
            }
            else
            {

                StudentiIndexVM model = new StudentiIndexVM
                {
                    Rows = _context.Studenti.Select(x => new StudentiIndexVM.Row
                    {
                        Id = x.Id,
                        Ime = x.Ime,
                        Prezime = x.Prezime,
                        Spol = x.Spol,
                        Jmbg = x.JMBG,
                        Grad = x._Grad.Naziv,
                        Email = x.Mail,
                    }).ToList()
                };
                return View(model);
            }
        }
    }
}