using Microsoft.AspNetCore.Mvc;
using SinglePage.Models;
using System.Diagnostics;

namespace SinglePage.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        
    }
}