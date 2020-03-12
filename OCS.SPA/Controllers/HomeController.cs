using Microsoft.AspNetCore.Mvc;
using OCS.SPA.Models;
using System.Diagnostics;

namespace OCS.SPA.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
