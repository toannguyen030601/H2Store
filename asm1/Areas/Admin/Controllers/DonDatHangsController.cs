using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using asm1.Entities;
using asm1.Models;

namespace asm1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/{controller}/{action}")]
    [Authentication]
    public class DonDatHangsController : Controller
    {
        private readonly AspmvcH2storeContext _context;

        public DonDatHangsController(AspmvcH2storeContext context)
        {
            _context = context;
        }

        // GET: Admin/DonDatHangs
        public async Task<IActionResult> Index()
        {
            var aspmvcH2storeContext = _context.DonDatHangChuaDktks.Include(d => d.MaDtNavigation);
            return View(await aspmvcH2storeContext.ToListAsync());
        }

        // GET: Admin/DonDatHangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donDatHangChuaDktk = await _context.DonDatHangChuaDktks
                .Include(d => d.MaDtNavigation)
                .FirstOrDefaultAsync(m => m.MaDh == id);
            if (donDatHangChuaDktk == null)
            {
                return NotFound();
            }

            return View(donDatHangChuaDktk);
        }

        // GET: Admin/DonDatHangs/Create
        public IActionResult Create()
        {
            ViewData["MaDt"] = new SelectList(_context.DienThoais, "MaDt", "MaDt");
            return View();
        }

        // POST: Admin/DonDatHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDh,HoTen,Email,Sdt,DiaChi,MaDt,SoLuong,NgayDat,TrangThai")] DonDatHangChuaDktk donDatHangChuaDktk)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donDatHangChuaDktk);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaDt"] = new SelectList(_context.DienThoais, "MaDt", "MaDt", donDatHangChuaDktk.MaDt);
            return View(donDatHangChuaDktk);
        }

        // GET: Admin/DonDatHangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donDatHangChuaDktk = await _context.DonDatHangChuaDktks.FindAsync(id);

            ViewBag.tenspdadat = _context.DienThoais.Where(p => p.MaDt == donDatHangChuaDktk.MaDt).Select(p => p.TenDt).FirstOrDefault();
            ViewBag.ramdadat = _context.DienThoais.Where(p => p.MaDt == donDatHangChuaDktk.MaDt).Select(p => p.Ram).FirstOrDefault();
            ViewBag.dungluongdadat = _context.DienThoais.Where(p => p.MaDt == donDatHangChuaDktk.MaDt).Select(p => p.DungLuong).FirstOrDefault();
            ViewBag.giadadat = _context.DienThoais.Where(p => p.MaDt == donDatHangChuaDktk.MaDt).Select(p => p.Gia).FirstOrDefault();
            ViewBag.hinhdadat = _context.DienThoais.Where(p => p.MaDt == donDatHangChuaDktk.MaDt).Select(p => p.HinhAnh).FirstOrDefault();
            ViewBag.thanhtien = ViewBag.giadadat * donDatHangChuaDktk.SoLuong;
            if (donDatHangChuaDktk == null)
            {
                return NotFound();
            }
            ViewData["MaDt"] = new SelectList(_context.DienThoais, "MaDt", "MaDt", donDatHangChuaDktk.MaDt);
            return View(donDatHangChuaDktk);
        }

        // POST: Admin/DonDatHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDh,HoTen,Email,Sdt,DiaChi,MaDt,SoLuong,NgayDat,TrangThai")] DonDatHangChuaDktk donDatHangChuaDktk)
        {
            if (id != donDatHangChuaDktk.MaDh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donDatHangChuaDktk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonDatHangChuaDktkExists(donDatHangChuaDktk.MaDh))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaDt"] = new SelectList(_context.DienThoais, "MaDt", "MaDt", donDatHangChuaDktk.MaDt);
            return View(donDatHangChuaDktk);
        }

        // GET: Admin/DonDatHangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donDatHangChuaDktk = await _context.DonDatHangChuaDktks
                .Include(d => d.MaDtNavigation)
                .FirstOrDefaultAsync(m => m.MaDh == id);
            if (donDatHangChuaDktk == null)
            {
                return NotFound();
            }

            return View(donDatHangChuaDktk);
        }

        // POST: Admin/DonDatHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donDatHangChuaDktk = await _context.DonDatHangChuaDktks.FindAsync(id);
            if (donDatHangChuaDktk != null)
            {
                _context.DonDatHangChuaDktks.Remove(donDatHangChuaDktk);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonDatHangChuaDktkExists(int id)
        {
            return _context.DonDatHangChuaDktks.Any(e => e.MaDh == id);
        }
    }
}
