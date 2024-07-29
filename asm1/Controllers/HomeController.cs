using asm1.Entities;
using asm1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace asm1.Controllers
{
    public class HomeController : Controller
    {

        private readonly AspmvcH2storeContext context;

        public HomeController(AspmvcH2storeContext context)
        {
            this.context = context;
        }
        
        public IActionResult Index()
        {
            ViewBag.Dienthoaiclient = context.DienThoais.ToList();
            return View();
        }

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
