using asm1.Entities;
using asm1.Models;
using asm1.ViewModels;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.Intrinsics.Arm;
using X.PagedList;

namespace asm1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/{controller}/{action}")]
    [Authentication]
    public class DienThoaiController : Controller
    {
        private readonly AspmvcH2storeContext context;
        private readonly IWebHostEnvironment environment;

        public DienThoaiController(AspmvcH2storeContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }
        public IActionResult Index(int? page, string TenDt)
        {
            int pageSize = 10   ;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var products = context.DienThoais.ToList();
            if (!string.IsNullOrEmpty(TenDt))
            {
                var timtendt = products.Where(p => p.TenDt.IndexOf(TenDt, StringComparison.OrdinalIgnoreCase) >= 0);
                PagedList<DienThoai> list = new PagedList<DienThoai>(timtendt, pageNumber, pageSize);
                return View(list);
            }
            else
            {
                PagedList<DienThoai> list = new PagedList<DienThoai>(products, pageNumber, pageSize);
                return View(list);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.MaLoaiDt = new SelectList(context.LoaiDienThoais.ToList(), "MaLoaiDt", "TenLoaiDt");
            return View();
        }

        [HttpPost]
        public IActionResult Create(asm1.ViewModels.DienThoaiDto dienthoaidto)
        {
            if(dienthoaidto.FileHinhAnh == null)
            {
                ModelState.AddModelError("FileHinhAnh", "Hình ảnh không được bỏ trống");
            }
            if(!ModelState.IsValid)
            {
                return View(dienthoaidto);
            }

            //luu hình ảnh
            string newFileName = DateTime.Now.ToString("ddMMyyyyHHmmssfff");
            newFileName += Path.GetExtension(dienthoaidto.FileHinhAnh!.FileName);

            string imgFullFath = environment.WebRootPath + "/images/Dienthoai/" + newFileName;
            using(var stream = System.IO.File.Create(imgFullFath))
            {
                dienthoaidto.FileHinhAnh.CopyTo(stream);
            }

            
            DienThoai dienThoai = new DienThoai()
            {
                TenDt = dienthoaidto.TenDt,
                Gia = dienthoaidto.Gia,
                Ram = dienthoaidto.Ram,
                DungLuong = dienthoaidto.DungLuong,
                MaLoaiDt = dienthoaidto.MaLoaiDt,
                HinhAnh = newFileName,
                Mota = dienthoaidto.Mota
            };

            context.DienThoais.Add(dienThoai);
            context.SaveChanges();
            return RedirectToAction("Index","DienThoai");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.MaLoaiDt = new SelectList(context.LoaiDienThoais.ToList(), "MaLoaiDt", "TenLoaiDt");
            var dienthoai = context.DienThoais.Find(id);
            if (dienthoai == null)
            {
                return RedirectToAction("Index", "DienThoai");
            }
            var dienthoaidto = new DienThoaiDto()
            {
                TenDt = dienthoai.TenDt,
                Gia = dienthoai.Gia,
                Ram = dienthoai.Ram,
                DungLuong = dienthoai.DungLuong,
                MaLoaiDt = dienthoai.MaLoaiDt,
                Mota = dienthoai.Mota
            };

            ViewData["MaDt"] = dienthoai.MaDt;
            ViewData["HinhAnh"] = dienthoai.HinhAnh;

            return View(dienthoaidto);
        }

        [HttpPost]
        public IActionResult Edit(int id, DienThoaiDto dienthoaidto)
        {
            var dienthoai = context.DienThoais.Find(id);
            if(dienthoai == null)
            {
                return RedirectToAction("Index", "DienThoai");
            }
            if (!ModelState.IsValid)
            {
                ViewData["MaDt"] = dienthoai.MaDt;
                ViewData["HinhAnh"] = dienthoai.HinhAnh;

                return View(dienthoaidto);
            }

            //update hình ảnh
            string newFileName = dienthoai.HinhAnh;
            if(dienthoaidto.FileHinhAnh != null)
            {
                newFileName = DateTime.Now.ToString("ddMMyyyyHHmmssfff");
                newFileName += Path.GetExtension(dienthoaidto.FileHinhAnh!.FileName);
                string imgFullFath = environment.WebRootPath + "/images/Dienthoai/" + newFileName;
                using (var stream = System.IO.File.Create(imgFullFath))
                {
                    dienthoaidto.FileHinhAnh.CopyTo(stream);
                }

                if (!string.IsNullOrEmpty(dienthoai.HinhAnh))
                {
                    string oldImgFullPath = environment.WebRootPath + "/images/Dienthoai/" + dienthoai.HinhAnh;
                    System.IO.File.Delete(oldImgFullPath);
                }
            }
            dienthoai.TenDt = dienthoaidto.TenDt;
            dienthoai.Gia = dienthoaidto.Gia;
            dienthoai.Ram = dienthoaidto.Ram;
            dienthoai.DungLuong = dienthoaidto.DungLuong;
            dienthoai.MaLoaiDt = dienthoaidto.MaLoaiDt;
            dienthoai.HinhAnh = newFileName;
            dienthoai.Mota = dienthoaidto.Mota;
            context.SaveChanges();


            return RedirectToAction("Index", "DienThoai");
        }

        public IActionResult Delete(int id)
        {
            var dienthoai = context.DienThoais.Find(id);
            if (dienthoai == null)
            {
                return RedirectToAction("Index", "DienThoai");
            }
            string oldImgFullPath = environment.WebRootPath + "/images/Dienthoai/" + dienthoai.HinhAnh;
            System.IO.File.Delete(oldImgFullPath);

            context.DienThoais.Remove(dienthoai);
            context.SaveChanges(true);

            return RedirectToAction("Index", "DienThoai");
        }
    }
}
