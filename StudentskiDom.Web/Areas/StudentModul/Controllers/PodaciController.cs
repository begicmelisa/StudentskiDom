using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentskiDom.Data.EF;
using StudentskiDom.Data.Models;
using StudentskiDom.Web.Areas.RecepcionerModul.ViewModels;
using StudentskiDom.Web.Helper;
using StudentskiDom.Web.Areas.StudentModul.ViewModels;


namespace StudentskiDom.Web.Areas.StudentModul.Controllers
{
	[Area("StudentModul")]
	public class PodaciController : Controller
    {
		MojContext _context;

		public PodaciController(MojContext db)
		{
			_context = db;
		}
		public IActionResult LicniPodaci()
        {
			KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
			Student student = _context.Studenti.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();

		
			if (korisnik == null || student == null)
			{
				TempData["error_poruka"] = "Nemate pravo pristupa!";
				return Redirect("/Autentifikacija/Index");
	}
	StudentiDetaljiVM model = new StudentiDetaljiVM();

	model=	_context.Studenti.Where(x=>x.Id==student.Id).Select(ss=> new StudentiDetaljiVM
			{
				Ime = ss.Ime,
				Prezime = ss.Prezime,
				Adresa = ss.Adresa,
				DatumRodjenja = ss.DatumRodjenja.ToString("dd.MM.yyyy"),
				DatumUpisa = _context.StudentiSobe.Where(x => x._StudentId == ss.Id).FirstOrDefault().DatumDodjele.ToString("dd.MM.yyyy"),
				Email = ss.Mail,
				Fakultet=ss._Fakultet.Naziv,
				Grad=ss._Grad.Naziv,
				Id=ss.Id,
				Jmbg=ss.JMBG,
				Password=ss.KorisnickiNalog.Lozinka,
				Username=ss.KorisnickiNalog.KorisnickoIme,
				Soba_= _context.StudentiSobe.Where(x => x._StudentId == ss.Id).FirstOrDefault()._Soba.Naziv,
				Spol=ss.Spol,
				Telefon=ss.Telefon


}).FirstOrDefault();

            return View("LicniPodaci", model);
        }

		public IActionResult KorisnickiRacunPodaci() {
			KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
			Student student = _context.Studenti.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
			if (korisnik == null || student == null)
			{
				TempData["error_poruka"] = "Nemate pravo pristupakljljlkj!";
				return Redirect("/Autentifikacija/Index");
			}
			StudentiDetaljiVM model = _context.Studenti.Where(x => x.Id == student.Id).Select(ss => new StudentiDetaljiVM
			{
				Ime = ss.Ime,
				Prezime = ss.Prezime,
				Adresa = ss.Adresa,
				DatumRodjenja = ss.DatumRodjenja.ToString("dd.MM.yyyy"),
				DatumUpisa = _context.StudentiSobe.Where(x => x._StudentId == ss.Id).FirstOrDefault().DatumDodjele.ToString("dd.MM.yyyy"),
				Email = ss.Mail,
				Fakultet = ss._Fakultet.Naziv,
				Grad = ss._Grad.Naziv,
				Id = ss.Id,
				Jmbg = ss.JMBG,
				Password = ss.KorisnickiNalog.Lozinka,
				Username = ss.KorisnickiNalog.KorisnickoIme,
				Soba_ = _context.StudentiSobe.Where(x => x._StudentId == ss.Id).FirstOrDefault()._Soba.Naziv,
				Spol = ss.Spol,
				Telefon = ss.Telefon


			}).FirstOrDefault();

			return View("KorisnickiRacunPodaci", model);

		}
		//public IActionResult PromjeniLozinku() {
		//	KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
		//	Student student = _context.Studenti.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
		//	if (korisnik == null || student == null)
		//	{
		//		TempData["error_poruka"] = "Nemate pravo pristupa!";
		//		return Redirect("/Autentifikacija/Index");
		//	}
		//	PodaciPromjeniLozinkuVM model = _context.KorisnickiNalozi.Where(x => x.Id == student.KorisnickiNalogId).Select(s => new PodaciPromjeniLozinkuVM {
		//		Username=s.KorisnickoIme
				

		//	}).FirstOrDefault();

		//	return View("PromjeniLozinku",model);
		//}
		//public IActionResult SnimiPromjene (PodaciPromjeniLozinkuVM model){
		//		KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
		//		Student student = _context.Studenti.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
		//		if (korisnik == null || student == null)
		//		{
		//			TempData["error_poruka"] = "Nemate pravo pristupa!";
		//			return Redirect("/Autentifikacija/Index");
		//		}
		//	KorisnickiNalog kn = _context.KorisnickiNalozi.Where(x => x.Id == student.KorisnickiNalogId).FirstOrDefault();
		//	kn.Lozinka = model.Password;
		//	_context.KorisnickiNalozi.Update(kn);
		//	_context.SaveChanges();

		//	return Redirect("/StudentModul/Podaci/KorisnickiRacunPodaci");
		//}

	}
}