﻿using Microsoft.AspNetCore.Mvc;

namespace Book.Areas.Admin.Controllers
{
    [Area("Admin")]
   
    public class HomeController : Controller
    {
    
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Denied()
        {
            return View();
        }
    }
}
