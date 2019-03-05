using Microsoft.AspNetCore.Mvc;
using SpyStore.Hol.Mvc.Models;
using System.Diagnostics;

namespace SpyStore.Hol.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Privacy()
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
