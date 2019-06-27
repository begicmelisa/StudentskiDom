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
	public class RezervacijaController : Controller
    {

		MojContext _context;

		public RezervacijaController(MojContext db)
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
			RezervacijeIndexVM model = new RezervacijeIndexVM
			{
				Rows = _context.RezervacijeSale.Select(x=>new RezervacijeIndexVM.Row {
					Id=x.Id,
					Datum=x.Datum,
					BrojSati=x.BrojSati,
					UkupnaCijena=x.BrojSati*x._Sala.CijenaPoSatu,
					sala=x._Sala.Naziv,
					zaposlenik=x._Zaposlenik.Ime+" "+x._Zaposlenik.Prezime,
					posjetilac=x._Posjetilac.Ime+" "+x._Posjetilac.Prezime

				}).OrderByDescending(s=>s.Datum).ToList()
			};
            return View("Index",model);
        }



		public IActionResult SaleDetalji()
		{
			KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
			Zaposlenik z = _context.Zaposlenici.Where(x => x.Id == korisnik.Id).FirstOrDefault();
			if (korisnik == null || z == null || z._VrstaZaposlenikaId != 1)
			{
				TempData["error_poruka"] = "Nemate pravo pristupa!";
				return Redirect("/Autentifikacija/Index");
			}
			SaleDetaljiVM model = new SaleDetaljiVM
			{
				Rows = _context.Sale.Select(x => new SaleDetaljiVM.Row
				{
					Id=x.Id,
					Kapacitet=x.Kapacitet,
					CijenaPoSatu=x.CijenaPoSatu,
					Naziv=x.Naziv
				}).ToList()
			};
			return View("SaleDetalji",model);
		}

		public IActionResult DodajPosjetioca() {
			KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
			Zaposlenik z = _context.Zaposlenici.Where(x => x.Id == korisnik.Id).FirstOrDefault();
			if (korisnik == null || z == null || z._VrstaZaposlenikaId != 1)
			{
				TempData["error_poruka"] = "Nemate pravo pristupa!";
				return Redirect("/Autentifikacija/Index");
			}
			PosjetilacDodajVM model = new PosjetilacDodajVM();

			return View("DodajPosjetioca",model);
		}

		public IActionResult SnimiDodajPosjetioca(PosjetilacDodajVM model)
		{
			KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
			Zaposlenik z = _context.Zaposlenici.Where(x => x.Id == korisnik.Id).FirstOrDefault();
			if (korisnik == null || z == null || z._VrstaZaposlenikaId != 1)
			{
				TempData["error_poruka"] = "Nemate pravo pristupa!";
				return Redirect("/Autentifikacija/Index");
			}
			Posjetilac noviPosjetilac = new Posjetilac
			{
				Ime = model.Ime,
				Prezime = model.Prezime,
				JMBG=model.JMBG,
				Mail=model.Mail,
				Telefon=model.Telefon
				
			};

			_context.Pojsjetioci.Add(noviPosjetilac);
			_context.SaveChanges();
			return Redirect("/RecepcionerModul/Rezervacija");
		}

		public IActionResult DodajRezervaciju(DateTime searchDate)
		{
			KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
			Zaposlenik z = _context.Zaposlenici.Where(x => x.Id == korisnik.Id).FirstOrDefault();
			if (korisnik == null || z == null || z._VrstaZaposlenikaId != 1)
			{
				TempData["error_poruka"] = "Nemate pravo pristupa!";
				return Redirect("/Autentifikacija/Index");
			}
			DodajRezervacijuVM model = new DodajRezervacijuVM
			{
				Zaposlenici = _context.Zaposlenici.Where(x => x._VrstaZaposlenika.Naziv == "Recepcioner").Select(p => new SelectListItem
				{
					Value = p.Id.ToString(),
					Text = p.Ime + " " + p.Prezime

				}).ToList(),

				Posjetioci = _context.Pojsjetioci.Select(p => new SelectListItem
				{
					Value = p.Id.ToString(),
					Text = p.Ime + " " + p.Prezime

				}).ToList(),
				Dvorane = _context.Sale.Select(p => new SelectListItem {
					Value = p.Id.ToString(),
					Text = p.Naziv
				}).ToList(),

				sale = _context.Sale.Select(p => new Sala
				{
				Id=p.Id,
				CijenaPoSatu=p.CijenaPoSatu,
				Kapacitet=p.Kapacitet,
				Naziv=p.Naziv

				}).ToList(),

				Datum=searchDate
				

			};

			List<RezervacijaSale> rezervacije = _context.RezervacijeSale.Where(x => x.Datum.ToString("dd.MM.yyyy") == searchDate.ToString("dd.MM.yyyy")).ToList();
		

			foreach (var item in rezervacije)
			{
				for (int i = 0; i < model.Dvorane.Count; i++)
				{
					if (item._SalaId.ToString() == model.Dvorane[i].Value)
					{
						model.Dvorane.Remove(model.Dvorane[i]);
						model.sale.Remove(model.sale[i]);
					}
				}

			}
			
			
		
			return View("DodajRezervaciju",model);
		}

		public IActionResult SnimiNovuRezervaciju(DodajRezervacijuVM model)
		{
			KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
			Zaposlenik z = _context.Zaposlenici.Where(x => x.Id == korisnik.Id).FirstOrDefault();
			if (korisnik == null || z == null || z._VrstaZaposlenikaId != 1)
			{
				TempData["error_poruka"] = "Nemate pravo pristupa!";
				return Redirect("/Autentifikacija/Index");
			}
			RezervacijaSale novaRezervacija = new RezervacijaSale();

			novaRezervacija.Datum = model.Datum;
			novaRezervacija.BrojSati = model.BrojSati;
			
			   novaRezervacija.UkupnaCijena = model.BrojSati * _context.Sale.Where(x => x.Id== model.DvoranaID).FirstOrDefault().CijenaPoSatu;
				novaRezervacija._SalaId = (int)model.DvoranaID;
				novaRezervacija._PosjetilacId = (int)model.PosjetilacID;
				novaRezervacija._ZaposlenikId = z.Id;

			

			_context.RezervacijeSale.Add(novaRezervacija);
			_context.SaveChanges();
			
			return Redirect("/RecepcionerModul/Rezervacija/Index");
		}

		public IActionResult ObrisiRezervaciju(int id)
		{
			KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
			Zaposlenik z = _context.Zaposlenici.Where(x => x.Id == korisnik.Id).FirstOrDefault();
			if (korisnik == null || z == null || z._VrstaZaposlenikaId != 1)
			{
				TempData["error_poruka"] = "Nemate pravo pristupa!";
				return Redirect("/Autentifikacija/Index");
			}
			RezervacijaSale rs = _context.RezervacijeSale.Where(x => x.Id == id).FirstOrDefault();

			_context.RezervacijeSale.Remove(rs);
			_context.SaveChanges();

			return Redirect("/RecepcionerModul/Rezervacija/Index");
		}

	}
}