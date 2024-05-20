﻿using database.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace database.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult home()
        {
            return View();
        }
        public IActionResult contact()
        {
            return View();
        }
        public IActionResult profile()
        {
            return View();
        }
        public IActionResult Faq()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}