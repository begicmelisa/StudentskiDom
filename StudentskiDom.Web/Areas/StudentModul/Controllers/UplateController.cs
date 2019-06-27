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
	public class UplateController : Controller
    {
		MojContext _context;

		public UplateController(MojContext db)
		{
			_context = db;
		}
		public IActionResult Index()
        {
			KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
			Student student = _context.Studenti.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
			if (korisnik == null || student == null)
			{
				TempData["error_poruka"] = "Nemate pravo pristupa!";
				return Redirect("/Autentifikacija/Index");
			}
			UplateIndexVM model = new UplateIndexVM { Rows = _context.Uplate.Where(x => x._StudentId == student.Id).Select(x => new UplateIndexVM.Row {
				Id=x.Id,
				Datum=x.Datum,
				Iznos=x.Iznos,
				StudentID=x._StudentId,
				zaposlenik=x._Zaposlenik.Ime+" "+x._Zaposlenik.Prezime,
				TipUplate=x._TipUplate.Naziv,
				Month=x.Datum.Month

			}).ToList() };
			model.Mjeseci = new List<string>
			{
				"Januar",
				"Februar",
				"Mart",
				"April",
				"Maj",
				"Juni",
				"Juli",
				"August",
				"Septebmar",
				"Oktobar",
				"Novembar",
				"Decembar"
			};



			return View("Index",model);
        }
    }
}