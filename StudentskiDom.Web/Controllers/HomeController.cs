using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentskiDom.Web.Models;
using StudentskiDom.Data.EF;
using StudentskiDom.Web.ViewModel;

namespace StudentskiDom.Web.Controllers
{
    public class HomeController : Controller
    {
        MojContext _context;
       public HomeController(MojContext db)
        {
            _context = db;
        }

        public IActionResult Index()
        {
            ViewData["ulazniModel"] = new HomeIndexVM
            {
                Rows = _context.Drzave.Select(x => new HomeIndexVM.Row
                {
                    DrzavaID = x.Id,
                    NazivDrzave=x.Naziv


                }).ToList()
            };
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
