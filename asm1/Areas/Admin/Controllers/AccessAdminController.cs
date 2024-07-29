using asm1.Entities;
using Microsoft.AspNetCore.Mvc;

namespace asm1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/{controller}/{action}")]
    public class AccessAdminController : Controller
    {
        AspmvcH2storeContext db = new AspmvcH2storeContext();
        public IActionResult LoginAdmin()
        {
            if (HttpContext.Session.GetString("UserNameAdmin") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult LoginAdmin(QuanTriVien quantrivien)
        {
            if (HttpContext.Session.GetString("UserNameAdmin") == null)
            {
                var u = db.QuanTriViens.Where(x => x.TaiKhoanQtv.Equals(quantrivien.TaiKhoanQtv) && x.MatKhauQtv.Equals(quantrivien.MatKhauQtv)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("UserNameAdmin", u.TaiKhoanQtv.ToString());
                    HttpContext.Session.SetString("TenQtv", u.TenQtv.ToString());


                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public IActionResult LogoutAdmin()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserNameAdmin");
            HttpContext.Session.Remove("TenQtv");
            return RedirectToAction("Index", "Home");
        }



    }
}
