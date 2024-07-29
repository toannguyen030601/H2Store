using Microsoft.AspNetCore.Mvc;
using asm1.Entities;
using asm1.Models;
using Microsoft.EntityFrameworkCore;

namespace asm1.Controllers
{
    public class AccessController : Controller
    {
        AspmvcH2storeContext db = new AspmvcH2storeContext();

        [HttpGet]
        public IActionResult Login()
        {
            if(HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }

        [HttpPost]
        public IActionResult Login(NguoiDung nguoiDung)
        {
            if(HttpContext.Session.GetString("UserName") == null)
            {
                var u = db.NguoiDungs.Where(x => x.TaiKhoanNd.Equals(nguoiDung.TaiKhoanNd) && x.MatKhauNd.Equals(nguoiDung.MatKhauNd)).FirstOrDefault();
                if(u != null)
                {
                    HttpContext.Session.SetString("UserName",u.TaiKhoanNd.ToString());
                    HttpContext.Session.SetString("EmailND", u.Email.ToString());
                    HttpContext.Session.SetString("TenND", u.TenNd.ToString());
                    HttpContext.Session.SetString("SDTND", u.Sdt.ToString());
                    HttpContext.Session.SetString("DiaChiND", u.DiaChi.ToString());

                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("cart");
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("TenND");
            HttpContext.Session.Remove("EmailND");
            HttpContext.Session.Remove("SDTND");
            HttpContext.Session.Remove("DiaChiND");
            return RedirectToAction("Index","Home");
        }


        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(NguoiDung nguoiDung)
        {
            if (string.IsNullOrEmpty(nguoiDung.Email))
            {
                TempData["Message"] = "Please enter your email.";
                return View();
            }
            var IsEmail = db.NguoiDungs.Where(u => u.Email == nguoiDung.Email).FirstOrDefault();
            if (IsEmail != null)
            {
                SendMail mail = new SendMail();
                string newPassword = mail.getPassword();
                mail.Send(nguoiDung.Email, newPassword, true);
                IsEmail.MatKhauNd = newPassword;
                db.SaveChanges();

                // Lưu thông điệp vào TempData
                TempData["Message"] = "Check your email for the new password.";

                return View();
            }
            else
            {
                // Nếu không tìm thấy email, thông báo lỗi
                TempData["Message"] = "Email not found.";
                return View();
            }
        }

        public IActionResult Taotaikhoannd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Taotaikhoannd(NguoiDung nguoiDung)
        {
            
            if (ModelState.IsValid)
            {
                var existingUser = db.NguoiDungs.FirstOrDefault(u => u.TaiKhoanNd == nguoiDung.TaiKhoanNd);
                var existingEmail = db.NguoiDungs.FirstOrDefault(u => u.Email == nguoiDung.Email);

                if (existingUser != null)
                {
                    // Tài khoản đã tồn tại, xử lý phản hồi tại đây (ví dụ: thông báo lỗi)
                    ModelState.AddModelError(string.Empty, "Tài khoản đã tồn tại.");
                    return View(nguoiDung);
                }
                if (existingEmail != null)
                {
                    ModelState.AddModelError(string.Empty, "Email đã tồn tại.");
                    return View(nguoiDung);
                }

                db.NguoiDungs.Add(nguoiDung);
                db.SaveChanges();

                return RedirectToAction("Login", "Access");
            }

            // If model validation fails, return the same view with the model to display validation errors
            return View(nguoiDung);
        }
    }
}
