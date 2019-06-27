using Microsoft.AspNetCore.Http;
using StudentskiDom.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.Extensions.DependencyInjection;
using StudentskiDom.Web.Helper;


namespace StudentskiDom.Web.Helper
{
    public static class Autentifikacija
    {
		private const string LogiraniKorisnik = "logirani_korisnik";

		public static void SetLogiraniKorisnik(this HttpContext context, KorisnickiNalog korisnik, bool snimiUCookie = false)
		{
			context.Session.Set(LogiraniKorisnik, korisnik);
		}


		public static KorisnickiNalog GetLogiraniKorisnik(this HttpContext context)
		{
			KorisnickiNalog korisnik = context.Session.Get<KorisnickiNalog>(LogiraniKorisnik);

			return korisnik;
		}
		}
}
