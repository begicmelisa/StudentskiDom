using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentskiDom.Data.EF;
using StudentskiDom.Data.Models;
using StudentskiDom.Web.Helper;
using StudentskiDom.Web.Areas.StudentModul.ViewModels;

namespace StudentskiDom.Web.Areas.StudentModul.Controllers
{
	[Area("StudentModul")]
	public class SobeController : Controller
    {
		private readonly MojContext _context;

		public SobeController(MojContext db)
		{
			_context = db;
		}

		public IActionResult Index()
        {
			KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
			Student ss = _context.Studenti.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
			int idSobe = _context.StudentiSobe.Where(x => x._StudentId == ss.Id).FirstOrDefault()._SobaId;
			if (korisnik == null || ss == null)
			{
				TempData["error_poruka"] = "Nemate pravo pristupa!";
				return Redirect("/Autentifikacija/Index");
			}
			SobeIndexVM model = new SobeIndexVM();
			model = _context.Sobe.Where(x => x.Id == idSobe).Select(d => new SobeIndexVM
			{
				Id=d.Id,
				Naziv=d.Naziv,
				PopunjenoKreveta=d.BrojKreveta,
				Sprat=d.Sprat,
				TipSobe=d._TipSobe.Naziv,
				Lista = _context.StudentiSobe.Where(r => r._SobaId == d.Id).Select(s => new StudentSoba
				{
					Id = s.Id,
					_Student = s._Student,
					_StudentId = s._StudentId,
					_SobaId = s._SobaId,
					DatumDodjele = s.DatumDodjele,
					_ZaposlenikId = s._ZaposlenikId,
					Napomena = s.Napomena


				}).ToList()
			}).FirstOrDefault();

			StudentSoba tajStudent = _context.StudentiSobe.Where(x=>x._StudentId==ss.Id).Select(s => new StudentSoba
			{
				Id = s.Id,
				_Student = s._Student,
				_StudentId = s._StudentId,
				_SobaId = s._SobaId,
				DatumDodjele = s.DatumDodjele,
				_ZaposlenikId = s._ZaposlenikId,
				Napomena = s.Napomena


			}).FirstOrDefault();

			model.Lista.Remove(tajStudent);

			return View("Index",model);
        }
    }
}