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

    public class RezervacijeController : Controller
    {
        MojContext _context;

            public RezervacijeController(MojContext db)
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
            RezervacijeIndexVM model = new RezervacijeIndexVM
            {
                Rows = _context.RezervacijeSale.Select(x => new RezervacijeIndexVM.Row
                {
                    Id = x.Id,
                    Datum = x.Datum,
                    BrojSati = x.BrojSati,
                    UkupnaCijena = x.BrojSati * x._Sala.CijenaPoSatu,
                    sala = x._Sala.Naziv,
                    zaposlenik = x._Zaposlenik.Ime + " " + x._Zaposlenik.Prezime,
                    posjetilac = x._Posjetilac.Ime + " " + x._Posjetilac.Prezime

                }).OrderByDescending(s => s.Datum).ToList()
            };

            return View(model);
        }
    }
}