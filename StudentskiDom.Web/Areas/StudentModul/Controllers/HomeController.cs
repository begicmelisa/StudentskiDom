using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentskiDom.Data.EF;
using StudentskiDom.Data.Models;
using StudentskiDom.Web.Helper;


namespace StudentskiDom.Web.Areas.StudentModul.Controllers
{
	[Area("StudentModul")]
    public class HomeController : Controller
    {
		private readonly MojContext _context;

		public HomeController(MojContext db)
		{
			_context = db;
		}

		public IActionResult Index()
		{
			KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
			Student ss = _context.Studenti.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
			if (korisnik == null || ss == null )
			{
				TempData["error_poruka"] = "Nemate pravo pristupa!";
				return Redirect("/Autentifikacija/Index");
			}
			return View();
		}
	}
}