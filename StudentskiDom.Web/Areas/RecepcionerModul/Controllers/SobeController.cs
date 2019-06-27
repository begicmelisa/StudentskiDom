using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentskiDom.Data.EF;
using StudentskiDom.Data.Models;
using StudentskiDom.Web.Areas.RecepcionerModul.ViewModels;
using StudentskiDom.Web.Helper;

namespace StudentskiDom.Web.Areas.RecepcionerModul.Controllers
{
	[Area("RecepcionerModul")]
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
			if (korisnik == null || z == null || z._VrstaZaposlenikaId != 1)
			{
				TempData["error_poruka"] = "Nemate pravo pristupa!";
				return Redirect("/Autentifikacija/Index");
			}
			SobeIndexVM model = new SobeIndexVM
			{
				Rows = _context.Sobe.Select(x => new SobeIndexVM.Row
				{
					Id = x.Id,
					Naziv = x.Naziv,
					Sprat = x.Sprat,
					TipSobe = x._TipSobe.Naziv,
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

		public IActionResult DodajStudenta(int id) {
			KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
			Zaposlenik zz = _context.Zaposlenici.Where(x => x.Id == korisnik.Id).FirstOrDefault();
			if (korisnik == null || zz == null || zz._VrstaZaposlenikaId != 1)
			{
				TempData["error_poruka"] = "Nemate pravo pristupa!";
				return Redirect("/Autentifikacija/Index");
			}
			SobeDodajStudentaVM model = new SobeDodajStudentaVM
			{
				SobaId = id,
				Naziv = _context.Sobe.Where(x => x.Id == id).FirstOrDefault().Naziv,

				Zaposlenici = _context.Zaposlenici.Where(z=>z._VrstaZaposlenika.Naziv=="Recepcioner").Select(s => new SelectListItem
				{
					Value = s.Id.ToString(),
					Text = s.Ime + " " + s.Prezime
				}).ToList(),

				 Studenti= _context.Studenti.Select(s => new SelectListItem
				{
					Value = s.Id.ToString(),
					Text = s.Ime + " " + s.Prezime
				}).ToList()


			};

			List<StudentSoba> listaZauzetihStudenata = _context.StudentiSobe.Select(x => new StudentSoba {
				_Student=x._Student,
				_StudentId=x._StudentId
			}).ToList();

			for (int i = 0; i < model.Studenti.Count; i++)
			{
				for (int j = 0; j < listaZauzetihStudenata.Count; j++)
				{
					if (model.Studenti[i].Value==listaZauzetihStudenata[j]._StudentId.ToString())
					{
						model.Studenti.Remove(model.Studenti[i]);
					}
				}
			}


			return View("DodajStudenta",model);
		}
		public IActionResult SnimiDodavanjeStudenta(SobeDodajStudentaVM model) {
			KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
			Zaposlenik z = _context.Zaposlenici.Where(x => x.Id == korisnik.Id).FirstOrDefault();
			if (korisnik == null || z == null || z._VrstaZaposlenikaId != 1)
			{
				TempData["error_poruka"] = "Nemate pravo pristupa!";
				return Redirect("/Autentifikacija/Index");
			}

			StudentSoba noviSS = new StudentSoba();

			noviSS.DatumDodjele = DateTime.Now;
			noviSS._SobaId = model.SobaId;
			noviSS._StudentId = (int)model.StudentID;
			noviSS._ZaposlenikId = z.Id;
			noviSS.Napomena = model.Napomena;
			_context.StudentiSobe.Add(noviSS);
			_context.SaveChanges();

			Soba s = _context.Sobe.Where(x => x.Id == model.SobaId).FirstOrDefault();
			s.BrojKreveta++;
			_context.Sobe.Update(s);
			_context.SaveChanges();

			return Redirect("/RecepcionerModul/Sobe/Index");
		}
		public IActionResult OdjaviStudenta(int id)
		{
			KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
			Zaposlenik z = _context.Zaposlenici.Where(x => x.Id == korisnik.Id).FirstOrDefault();
			if (korisnik == null || z == null || z._VrstaZaposlenikaId != 1)
			{
				TempData["error_poruka"] = "Nemate pravo pristupa!";
				return Redirect("/Autentifikacija/Index");
			}
			StudentSoba ss = _context.StudentiSobe.Where(x => x.Id == id).FirstOrDefault();
			Soba s = _context.Sobe.Where(x => x.Id == ss._SobaId).FirstOrDefault();
			s.BrojKreveta--;
			_context.Sobe.Update(s);
			_context.SaveChanges();
			_context.StudentiSobe.Remove(ss);
			_context.SaveChanges();

			return Redirect("/RecepcionerModul/Sobe");
		}
	}
}