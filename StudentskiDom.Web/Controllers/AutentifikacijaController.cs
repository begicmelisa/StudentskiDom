using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentskiDom.Data.EF;
using StudentskiDom.Data.Models;
using StudentskiDom.Web.Helper;
using StudentskiDom.Web.ViewModel;

namespace StudentskiDom.Web.Controllers
{
    public class AutentifikacijaController : Controller
    {
       

		private readonly MojContext _db;

		public AutentifikacijaController(MojContext db)
		{
			_db = db;
		}

		public IActionResult Index()
		{
			return View(new LoginVM()
			{
				ZapamtiPassword = true,
			});
		}

		public IActionResult Login(LoginVM input)
		{
			KorisnickiNalog korisnik = _db.KorisnickiNalozi
				.SingleOrDefault(x => x.KorisnickoIme == input.username && x.Lozinka == input.password);

			if (korisnik == null)
			{
				TempData["error_poruka"] = "pogrešan username ili password";
				return View("Index", input);
			}

			HttpContext.SetLogiraniKorisnik(korisnik, input.ZapamtiPassword);

			bool isStudent=false, isRecepcioner=false, isManager=false;

			Student s = _db.Studenti.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
			Zaposlenik z= _db.Zaposlenici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();

			if (s!=null)
			{
				isStudent = true;
			}
			else if (z!=null)
			{
				if (z._VrstaZaposlenikaId==1) //1 jer je u bazi Recepcioner sa id 1
				{
					isRecepcioner = true;
				}
				else
				{
					isManager = true;
				}
			}

			if (isStudent)
			{
				return Redirect("/StudentModul/Home/Index");
			}
			if (isRecepcioner)
			{
				return Redirect("/RecepcionerModul/Home/Index");
			}

			else 
			{
				return Redirect("/ManagerModul/Home/Index");
				//isManager jos uvijek ne postoji ovaj modul
			}
			

			
		}

		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index");
		}
	}
}