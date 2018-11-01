using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SistemaDeVentas.Controllers
{
    public class ErrorController : Controller
    {
        //[Route("/Error")] 
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.Value == 404 || statusCode.Value == 500)
            {
                ViewData["Error"] = statusCode.ToString();
            }
            return View();
        }
    }
}