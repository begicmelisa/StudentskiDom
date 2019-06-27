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
    public class SobeController : Controller
    {
        MojContext _context;

        public SobeController(MojContext db)
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
            int idTip;
            SobeIndexVM model = new SobeIndexVM
            {
                
                Rows = _context.Sobe.Select(x => new SobeIndexVM.Row
                {
                    Id = x.Id,
                    Naziv = x.Naziv,
                    Sprat = x.Sprat,
                    TipSobe = x._TipSobe.Naziv,
                    IdTipSobe=x._TipSobeId,
                    PopunjenoKreveta = x.BrojKreveta,
                    Lista = _context.StudentiSobe.Where(r => r._SobaId == x.Id).Select(s => new StudentSoba
                    {
                        Id = s.Id,
                        _Student = s._Student,
                        _StudentId = s._StudentId,
                        _SobaId = s._SobaId,
                        DatumDodjele = s.DatumDodjele,
                        _ZaposlenikId = s._ZaposlenikId,
                        Napomena = s.Napomena
                    }).ToList()
                }).ToList()

            };
            return View(model);
        }
    }
}