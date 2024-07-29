using asm1.Entities;
using asm1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asm1.Controllers
{
    public class GioHangController : Controller
    {
        private readonly AspmvcH2storeContext context;

        public GioHangController(AspmvcH2storeContext context)
        {
            this.context = context;
        }

        private int isExit(int id)
        {
            List<Item> cart = SessionHelper.GetOjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].dienthoai.MaDt.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

        public IActionResult Buy(int id)
        {
            DienThoaiModel dienThoaiModel = new DienThoaiModel();
            if (SessionHelper.GetOjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { dienthoai = dienThoaiModel.Find(id), quantity = 1 });
                SessionHelper.SetOjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Item> cart = SessionHelper.GetOjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExit(id);
                if (index != -1)
                {
                    cart[index].quantity++;
                }
                else
                {
                    cart.Add(new Item { dienthoai = dienThoaiModel.Find(id), quantity = 1 });
                }
                SessionHelper.SetOjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            List<Item> cart = SessionHelper.GetOjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExit(id);
            cart.RemoveAt(index);
            SessionHelper.SetOjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var cart = SessionHelper.GetOjectFromJson<List<Item>>(HttpContext.Session, "cart");

            if (cart != null && cart.Count > 0)
            {
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(x => x.dienthoai.Gia * x.quantity);
            }
            else
            {
                ViewBag.cart = new List<Item>();
                ViewBag.total = 0;
                ViewBag.Message = "Chưa có đơn hàng.";
            }
            return View();
        }

        public IActionResult DatHang(string email,string hoten, string sodienthoai,string diachi)
        {
            var carts = SessionHelper.GetOjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if (carts == null || carts.Count == 0)
            {
                // Nếu giỏ hàng trống, chuyển hướng đến trang thông báo
                return RedirectToAction("Index");
            }

            if(HttpContext.Session.GetString("UserName") != null)
            {
                email = HttpContext.Session.GetString("EmailND");
                hoten = HttpContext.Session.GetString("TenND");
                sodienthoai = HttpContext.Session.GetString("SDTND");
                diachi = HttpContext.Session.GetString("DiaChiND");
            }
            
            foreach (var cart in carts)
            {
                var donHang = new DonDatHangChuaDktk()
                {
                    Email = email,
                    HoTen = hoten,
                    Sdt = sodienthoai,
                    DiaChi = diachi,
                    SoLuong = cart.quantity,
                    MaDt = cart.dienthoai.MaDt,
                    /*TongTien = cart.Sum(item => item.DienThoai.Gia * item.Quantity),*/
                    NgayDat = DateTime.Now,
                    TrangThai = false
                };

                context.DonDatHangChuaDktks.Add(donHang);
                context.SaveChanges();
            }

            HttpContext.Session.Remove("cart");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult GioHang()
        {

            return View();
        }
    }
}
