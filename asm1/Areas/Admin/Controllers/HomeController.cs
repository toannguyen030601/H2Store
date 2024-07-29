using asm1.Models;
using Microsoft.AspNetCore.Mvc;

namespace asm1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    [Route("Admin/Home")]
    [Authentication]
    public class HomeController : Controller
    {
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        

        [Route("DonDatHang")]
        public IActionResult DonDatHang()
        {
            return View();
        }
    }
}
