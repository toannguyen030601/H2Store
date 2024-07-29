using asm1.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace asm1.Controllers
{
    public class DienThoaiClientController : Controller
    {
        private readonly AspmvcH2storeContext context;

        public DienThoaiClientController(AspmvcH2storeContext context)
        {
            this.context = context;
        }
        public IActionResult Index(int danhmuc, int? page, string TenDt, string priceRange, string ramdt, string dungluong)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var dienthoai = context.DienThoais.ToList();
            ViewBag.Loaidt = context.LoaiDienThoais.ToList();

            if (danhmuc != 0)
            {
                dienthoai = dienthoai.Where(x => x.MaLoaiDt == danhmuc).ToList();

            }
            if (!string.IsNullOrEmpty(TenDt))
            {
                dienthoai = dienthoai.Where(p => p.TenDt.IndexOf(TenDt, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }

            if (!string.IsNullOrEmpty(ramdt))
            {
                dienthoai = dienthoai.Where(x => x.Ram == ramdt).ToList();
            }
            if (!string.IsNullOrEmpty(dungluong))
            {
                dienthoai = dienthoai.Where(x => x.DungLuong == dungluong).ToList();
            }

            if (!string.IsNullOrEmpty(priceRange))
            {
                switch (priceRange)
                {
                    case "thapdencao":
                        dienthoai = context.DienThoais.OrderBy(p => p.Gia).ToList();
                        break;
                    case "duoi2tr":
                        dienthoai = dienthoai.Where(p => p.Gia < 2000000).ToList();
                        break;
                    case "2den4tr":
                        dienthoai = dienthoai.Where(p => p.Gia >= 2000000 && p.Gia <= 4000000).ToList();
                        break;
                    case "tren4tr":
                        dienthoai = dienthoai.Where(p => p.Gia > 4000000).ToList();
                        break;
                    case "caodenthap":
                        dienthoai = context.DienThoais.OrderByDescending(p => p.Gia).ToList();
                        break;
                }
            }



            ViewBag.DanhMuc = danhmuc;
            ViewBag.TenDt = TenDt;
            ViewBag.PriceRange = priceRange;
            ViewBag.RamDt = ramdt;
            ViewBag.DungLuong = dungluong;

            PagedList<DienThoai> list = new PagedList<DienThoai>(dienthoai, pageNumber, pageSize);
            return View(list);
        }


        public IActionResult Detail(int id)
        {
            var dienthoaiDetail = context.DienThoais.Find(id);
            return View(dienthoaiDetail);
        }
    }
}
