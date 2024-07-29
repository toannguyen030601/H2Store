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
    public class QuanTriViensController : Controller
    {
        private readonly AspmvcH2storeContext _context;

        public QuanTriViensController(AspmvcH2storeContext context)
        {
            _context = context;
        }

        // GET: Admin/QuanTriViens
        public async Task<IActionResult> Index()
        {
            return View(await _context.QuanTriViens.ToListAsync());
        }

        // GET: Admin/QuanTriViens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quanTriVien = await _context.QuanTriViens
                .FirstOrDefaultAsync(m => m.MaQtv == id);
            if (quanTriVien == null)
            {
                return NotFound();
            }

            return View(quanTriVien);
        }

        // GET: Admin/QuanTriViens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/QuanTriViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaQtv,TaiKhoanQtv,MatKhauQtv,TenQtv")] QuanTriVien quanTriVien)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _context.QuanTriViens.FirstOrDefaultAsync(u => u.TaiKhoanQtv == quanTriVien.TaiKhoanQtv);
                if (existingUser != null)
                {
                    // Tài khoản đã tồn tại, xử lý phản hồi tại đây (ví dụ: thông báo lỗi)
                    ModelState.AddModelError(string.Empty, "Tài khoản đã tồn tại.");
                    return View(quanTriVien);
                }

                // Tài khoản không tồn tại, thêm quản trị viên mới
                _context.Add(quanTriVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quanTriVien);
        }

        // GET: Admin/QuanTriViens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quanTriVien = await _context.QuanTriViens.FindAsync(id);
            if (quanTriVien == null)
            {
                return NotFound();
            }
            return View(quanTriVien);
        }

        // POST: Admin/QuanTriViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaQtv,TaiKhoanQtv,MatKhauQtv,TenQtv")] QuanTriVien quanTriVien)
        {
            if (id != quanTriVien.MaQtv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quanTriVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuanTriVienExists(quanTriVien.MaQtv))
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
            return View(quanTriVien);
        }


        public IActionResult Delete(int id)
        {
            var qtv = _context.QuanTriViens.Find(id);
            if (qtv == null)
            {
                return RedirectToAction("Index", "QuanTriViens");
            }

            _context.QuanTriViens.Remove(qtv);
            _context.SaveChanges();

            return RedirectToAction("Index", "QuanTriViens");
        }

        // GET: Admin/QuanTriViens/Delete/5
        /*public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quanTriVien = await _context.QuanTriViens
                .FirstOrDefaultAsync(m => m.MaQtv == id);
            if (quanTriVien == null)
            {
                return NotFound();
            }

            return View(quanTriVien);
        }

        // POST: Admin/QuanTriViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quanTriVien = await _context.QuanTriViens.FindAsync(id);
            if (quanTriVien != null)
            {
                _context.QuanTriViens.Remove(quanTriVien);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/

        private bool QuanTriVienExists(int id)
        {
            return _context.QuanTriViens.Any(e => e.MaQtv == id);
        }
    }
}
