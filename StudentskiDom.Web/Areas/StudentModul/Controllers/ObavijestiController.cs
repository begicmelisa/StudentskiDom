using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentskiDom.Data.EF;
using StudentskiDom.Data.Models;
using StudentskiDom.Web.Areas.RecepcionerModul.ViewModels;
using StudentskiDom.Web.Helper;


namespace StudentskiDom.Web.Areas.StudentModul.Controllers
{
	[Area("StudentModul")]
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
			Student ss = _context.Studenti.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
			if (korisnik == null || ss == null)
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
	}
}