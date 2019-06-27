using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using StudentskiDom.Data.EF;
using StudentskiDom.Data.Models;
using StudentskiDom.Web.Areas.RecepcionerModul.ViewModels;
using StudentskiDom.Web.Helper;

namespace StudentskiDom.Web.Areas.RecepcionerModul.Controllers
{
	[Area("RecepcionerModul")]
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
			if (korisnik == null || z == null || z._VrstaZaposlenikaId != 1)
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
						Soba_ = _context.StudentiSobe.Where(s => s._StudentId == x.Id).FirstOrDefault()._Soba.Naziv

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
						Soba_ = _context.StudentiSobe.Where(s => s._StudentId == x.Id).FirstOrDefault()._Soba.Naziv
					}).ToList()
				};
				return View("Index", model);
			}
		}

		public IActionResult Dodaj()
		{
			KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
			Zaposlenik z = _context.Zaposlenici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
			if (korisnik == null || z == null || z._VrstaZaposlenikaId != 1)
			{
				TempData["error_poruka"] = "Nemate pravo pristupa!";
				return Redirect("/Autentifikacija/Index");
			}
			StudentiDodajVM model = new StudentiDodajVM
			{

				Gradovi = _context.Gradovi.Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.Naziv

				}).ToList(),

				Fakulteti = _context.Fakulteti.Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.Naziv

				}).ToList(),


				SlobodneSobe = _context.Sobe.Where(x => x.BrojKreveta < 3).Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.Naziv + x._TipSobe.Naziv

				}).ToList()

			};
			SelectListItem n = new SelectListItem
			{
				Value = null,
				Text = "Nije odabrano"
			};

			model.SlobodneSobe.Add(n);
			model.Fakulteti.Add(n);
			model.Gradovi.Add(n);

			return View("Dodaj", model);
		}

		public IActionResult Snimi(StudentiDodajVM model)
		{
			KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
			Zaposlenik z = _context.Zaposlenici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
			if (korisnik == null || z == null || z._VrstaZaposlenikaId != 1)
			{
				TempData["error_poruka"] = "Nemate pravo pristupa!";
				return Redirect("/Autentifikacija/Index");
			}
			if (!ModelState.IsValid)
			{
				model.Gradovi = _context.Gradovi.Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.Naziv

				}).ToList();

				model.Fakulteti = _context.Fakulteti.Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.Naziv

				}).ToList();

				model.SlobodneSobe = _context.Sobe.Where(x => x.BrojKreveta <= 3).Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.Naziv + x._TipSobe.Naziv

				}).ToList();

				SelectListItem n = new SelectListItem
				{
					Value = null,
					Text = "Nije odabrano"
				};

				model.SlobodneSobe.Add(n);
				model.Fakulteti.Add(n);
				model.Gradovi.Add(n);

				return View("Dodaj", model);
			}

			Student noviStudent = new Student()
			{
				Ime = model.Ime,
				Prezime = model.Prezime,
				Spol = model.Spol,
				JMBG = model.Jmbg,
				Mail = model.Email,
				Adresa = model.Adresa,
				DatumRodjenja = (DateTime)model.DatumRodjenja,
				Telefon = model.Telefon,
				_GradId = model.GradId,
				_FakultetId = model.FakultetId

			};
			_context.Studenti.Add(noviStudent);
			_context.SaveChanges();

			if (model.SlobodnaSobaId != null)
			{
				StudentSoba noviStudentSoba = new StudentSoba();
				noviStudentSoba.DatumDodjele = DateTime.Now;
				noviStudentSoba._StudentId = noviStudent.Id;
				noviStudentSoba._SobaId = (int)model.SlobodnaSobaId;
				
				noviStudentSoba._ZaposlenikId = 1;  //moram ovako jer nije dozvoljeno null, promjeniti kad budemo autentifikaicju
				_context.StudentiSobe.Add(noviStudentSoba);
				_context.SaveChanges();


				Soba izabranaSoba = _context.Sobe.Where(x => x.Id == model.SlobodnaSobaId).FirstOrDefault();
				izabranaSoba.BrojKreveta++;
				_context.Sobe.Update(izabranaSoba);
				_context.SaveChanges();
			}

			KorisnickiNalog noviNalog = new KorisnickiNalog();
			noviNalog.KorisnickoIme = noviStudent.Ime + "." + noviStudent.Prezime;
			noviNalog.Lozinka = "0000";
			_context.KorisnickiNalozi.Add(noviNalog);
			_context.SaveChanges();

			noviStudent.KorisnickiNalogId = noviNalog.Id;
			_context.Studenti.Update(noviStudent);
			_context.SaveChanges();
			return Redirect("/RecepcionerModul/Studenti/Index");
		}

		public IActionResult Obrisi(int id)
		{
			KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
			Zaposlenik z = _context.Zaposlenici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
			if (korisnik == null || z == null || z._VrstaZaposlenikaId != 1)
			{
				TempData["error_poruka"] = "Nemate pravo pristupa!";
				return Redirect("/Autentifikacija/Index");
			}
			Student s = _context.Studenti.Where(x => x.Id == id).FirstOrDefault();

			StudentSoba ss = _context.StudentiSobe.Where(x => x._StudentId == id).FirstOrDefault(); //Soba u kojoj je bio kako bi oslobodili mjesto
			if (ss != null)
			{
				int idSobe = ss._SobaId;
				Soba soba = _context.Sobe.Where(x => x.Id == idSobe).FirstOrDefault();
				if (soba.BrojKreveta != 0)
				{
					soba.BrojKreveta--;
				}


				_context.StudentiSobe.Remove(ss);

				_context.Sobe.Update(soba);
				_context.SaveChanges();
			}

			KorisnickiNalog kn = _context.KorisnickiNalozi.Where(x => x.KorisnickoIme == s.Ime + "." + s.Prezime).FirstOrDefault();
			if (kn != null)
			{
				_context.KorisnickiNalozi.Remove(kn);
				_context.SaveChanges();
			}
			_context.Studenti.Remove(s);
			_context.SaveChanges();


			return Redirect("/RecepcionerModul/Studenti/Index");
		}

		public IActionResult Detalji(int id)
		{
			KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
			Zaposlenik z = _context.Zaposlenici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
			if (korisnik == null || z == null || z._VrstaZaposlenikaId != 1)
			{
				TempData["error_poruka"] = "Nemate pravo pristupa!";
				return Redirect("/Autentifikacija/Index");
			}
			int sobaId;
			StudentSoba stSo = _context.StudentiSobe.Where(s => s._StudentId == id).FirstOrDefault();
			if (stSo != null)
			{
				sobaId = stSo._SobaId;
			}

			StudentiDetaljiVM model = _context.Studenti.Where(x => x.Id == id).Select(x => new StudentiDetaljiVM
			{
				Id = x.Id,
				Ime = x.Ime,
				Prezime = x.Prezime,
				Spol = x.Spol,
				Jmbg = x.JMBG,
				Grad = x._Grad.Naziv,
				Email = x.Mail,
				Soba_ = _context.StudentiSobe.Where(s => s._StudentId == x.Id).FirstOrDefault()._Soba.Naziv,
				Fakultet = x._Fakultet.Naziv,
				DatumRodjenja = x.DatumRodjenja.ToString(),
				DatumUpisa = _context.StudentiSobe.Where(s => s._StudentId == id).FirstOrDefault().DatumDodjele.ToString(),
				Username = _context.KorisnickiNalozi.Where(s => s.KorisnickoIme == x.Ime + "." + x.Prezime).FirstOrDefault().KorisnickoIme,
				Password = _context.KorisnickiNalozi.Where(s => s.KorisnickoIme == x.Ime + "." + x.Prezime).FirstOrDefault().Lozinka,
				Adresa = x.Adresa,
				Telefon = x.Telefon

			}).FirstOrDefault();

			//List<StudentSoba> studenti = _context.StudentiSobe.Where(s => s._SobaId == sobaId).ToList();
			//if (studenti != null)
			//{
			//	foreach (var x in studenti)
			//	{
			//		model.studentiSaKojimaDijeliSobu.Add(x._Student.Ime + " " + x._Student.Prezime);
			//	}
			//}

			return View("Detalji", model);
		}

		public IActionResult Edit(int id)
		{
			KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
			Zaposlenik z = _context.Zaposlenici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
			if (korisnik == null || z == null || z._VrstaZaposlenikaId != 1)
			{
				TempData["error_poruka"] = "Nemate pravo pristupa!";
				return Redirect("/Autentifikacija/Index");
			}

			StudentiUrediVM model = _context.Studenti.Where(s => s.Id == id).Select(x => new StudentiUrediVM
			{
				Id = x.Id,
				Ime = x.Ime,
				Prezime = x.Prezime,
				Spol = x.Spol,
				Jmbg = x.JMBG,

				Email = x.Mail,
				Soba_ = _context.StudentiSobe.Where(s => s._StudentId == x.Id).FirstOrDefault()._Soba.Naziv,

				DatumRodjenja = x.DatumRodjenja.ToString(),
				DatumUpisa = _context.StudentiSobe.Where(s => s._StudentId == id).FirstOrDefault().DatumDodjele.ToString(),
				Username = _context.KorisnickiNalozi.Where(s => s.KorisnickoIme == x.Ime + "." + x.Prezime).FirstOrDefault().KorisnickoIme,
				Password = _context.KorisnickiNalozi.Where(s => s.KorisnickoIme == x.Ime + "." + x.Prezime).FirstOrDefault().Lozinka,
				Adresa = x.Adresa,
				Telefon = x.Telefon


			}).FirstOrDefault();

			model.Gradovi = _context.Gradovi.Select(x => new SelectListItem
			{
				Value = x.Id.ToString(),
				Text = x.Naziv

			}).ToList();

			model.Fakulteti = _context.Fakulteti.Select(x => new SelectListItem
			{
				Value = x.Id.ToString(),
				Text = x.Naziv

			}).ToList();
			return View("Edit", model);


		}


		public IActionResult SnimiPromjene(StudentiUrediVM model)
		{
			KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
			Zaposlenik z = _context.Zaposlenici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
			if (korisnik == null || z == null || z._VrstaZaposlenikaId != 1)
			{
				TempData["error_poruka"] = "Nemate pravo pristupa!";
				return Redirect("/Autentifikacija/Index");
			}
			if (!ModelState.IsValid)
			{
				model.Gradovi = _context.Gradovi.Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.Naziv

				}).ToList();

				model.Fakulteti = _context.Fakulteti.Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.Naziv

				}).ToList();

				return View("Edit", model);
			}

			Student s = _context.Studenti.Where(x => x.Id == model.Id).FirstOrDefault();


			s.Ime = model.Ime;
			s.Prezime = model.Prezime;
			s.Spol = model.Spol;
			s.JMBG = model.Jmbg;
			s._FakultetId = model.FakultetId;
			s._GradId = model.GradId;
			s.Mail = model.Email;


			//s.DatumRodjenja = model.DatumRodjenja;
			s.DatumRodjenja = DateTime.Now;
			KorisnickiNalog kn = _context.KorisnickiNalozi.Where(w => w.KorisnickoIme == s.Ime + "." + s.Prezime).FirstOrDefault();
			if (kn == null)
			{
				kn = new KorisnickiNalog
				{
					KorisnickoIme = model.Username,
					Lozinka = model.Password
				};
			}
			else
			{
				kn.KorisnickoIme = model.Username;
				kn.Lozinka = model.Password;
			}

			s.Adresa = model.Adresa;
			s.Telefon = model.Telefon;

			_context.Studenti.Update(s);
			_context.KorisnickiNalozi.Update(kn);
			_context.SaveChanges();

			return Redirect("/RecepcionerModul/Studenti/Detalji?id=" + model.Id);

		}

		public IActionResult DodajUSobu(int id)
		{
			KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
			Zaposlenik z = _context.Zaposlenici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
			if (korisnik == null || z == null || z._VrstaZaposlenikaId != 1)
			{
				TempData["error_poruka"] = "Nemate pravo pristupa!";
				return Redirect("/Autentifikacija/Index");
			}
			StudentiDodajUSobuVM model = _context.Studenti.Where(x => x.Id == id).Select(s => new StudentiDodajUSobuVM
			{
				StudentId=s.Id,
				ImeiPrezime=s.Ime+" "+s.Prezime,
				Zaposlenici= _context.Zaposlenici.Where(x => x._VrstaZaposlenika.Naziv == "Recepcioner").Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.Ime + " " + x.Prezime

				}).ToList(),
			SlobodneSobe = _context.Sobe.Where(x => x.BrojKreveta <= 3).Select(x => new SelectListItem
					{
						Value = x.Id.ToString(),
						Text = x.Naziv +" " +x._TipSobe.Naziv

					}).ToList()

		}).FirstOrDefault();
		

			return PartialView("DodajUSobu", model);
		}

		public IActionResult SnimiDodavanjeUSobu(StudentiDodajUSobuVM model)
		{
			KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
			Zaposlenik z = _context.Zaposlenici.Where(x => x.KorisnickiNalogId == korisnik.Id).FirstOrDefault();
			if (korisnik == null || z == null || z._VrstaZaposlenikaId != 1)
			{
				TempData["error_poruka"] = "Nemate pravo pristupa!";
				return Redirect("/Autentifikacija/Index");
			}
			if (!ModelState.IsValid)
			{


				model.SlobodneSobe = _context.Sobe.Where(x => x.BrojKreveta <= 3).Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.Naziv + x._TipSobe.Naziv

				}).ToList();

				model.Zaposlenici = _context.Zaposlenici.Where(p => p._VrstaZaposlenika.Naziv == "Recepcioner").Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.Ime +" " +x.Prezime

				}).ToList();

				return View("DodajUSobu", model);
			}

			StudentSoba noviSS = new StudentSoba
			{
				_StudentId = model.StudentId,
				_SobaId = (int)model.SlobodnaSobaId,

				//_ZaposlenikId=1,
				_ZaposlenikId = z.Id
			};

			_context.StudentiSobe.Add(noviSS);
			_context.SaveChanges();

			Soba s = _context.Sobe.Where(x => x.Id == noviSS._SobaId).FirstOrDefault();
			s.BrojKreveta++;
			_context.Sobe.Update(s);
			_context.SaveChanges();

				return Redirect("/RecepcionerModul/Studenti/Index");
		}
	}
}