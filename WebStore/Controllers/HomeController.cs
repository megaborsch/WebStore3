﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        

        public HomeController()
        {
            
        }

        public async Task<IActionResult> Index()
        {
            //throw new InvalidOperationException("Ошибка!");
            return View();
        }


        //public IActionResult Details()
        //{
        //    return View();
        //}

        //public IActionResult Shop() { return View(); }

        //public IActionResult Login() { return View(); }
        public IActionResult Contact() { return View(); }
        public IActionResult Checkout() { return View(); }
        
        public IActionResult BlogSingle()
        {
            return View();
        }
        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult ErrorStatus(string id)
        {
            if (id == "404")
                return RedirectToAction("NotFound");
            return Content($"Статуcный код ошибки: {id}");
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult NotFound()
        {
            return View();
        }

    }
}