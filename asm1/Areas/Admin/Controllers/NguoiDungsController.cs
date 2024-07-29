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
    public class NguoiDungsController : Controller
    {
        private readonly AspmvcH2storeContext _context;

        public NguoiDungsController(AspmvcH2storeContext context)
        {
            _context = context;
        }

        // GET: Admin/NguoiDungs
        public async Task<IActionResult> Index()
        {
            return View(await _context.NguoiDungs.ToListAsync());
        }

        // GET: Admin/NguoiDungs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDungs
                .FirstOrDefaultAsync(m => m.MaNd == id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            return View(nguoiDung);
        }

        // GET: Admin/NguoiDungs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/NguoiDungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNd,TenNd,TaiKhoanNd,MatKhauNd,Email,Sdt,DiaChi")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _context.NguoiDungs.FirstOrDefaultAsync(u => u.TaiKhoanNd == nguoiDung.TaiKhoanNd);
                var existingEmail = await _context.NguoiDungs.FirstOrDefaultAsync(u => u.Email == nguoiDung.Email);
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
                _context.Add(nguoiDung);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

                // Tài khoản không tồn tại, thêm người dùng mới

            }
            return View(nguoiDung);
        }

        // GET: Admin/NguoiDungs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDungs.FindAsync(id);
            if (nguoiDung == null)
            {
                return NotFound();
            }
            return View(nguoiDung);
        }

        // POST: Admin/NguoiDungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaNd,TenNd,TaiKhoanNd,MatKhauNd,Email,Sdt,DiaChi")] NguoiDung nguoiDung)
        {
            if (id != nguoiDung.MaNd)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nguoiDung);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NguoiDungExists(nguoiDung.MaNd))
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
            return View(nguoiDung);
        }

        // GET: Admin/NguoiDungs/Delete/5

        public IActionResult Delete(int id)
        {
            var kh = _context.NguoiDungs.Find(id);
            if (kh == null)
            {
                return RedirectToAction("Index", "NguoiDungs");
            }

            _context.NguoiDungs.Remove(kh);
            _context.SaveChanges();

            return RedirectToAction("Index", "NguoiDungs");
        }
        /*public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDungs
                .FirstOrDefaultAsync(m => m.MaNd == id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            return View(nguoiDung);
        }

        // POST: Admin/NguoiDungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nguoiDung = await _context.NguoiDungs.FindAsync(id);
            if (nguoiDung != null)
            {
                _context.NguoiDungs.Remove(nguoiDung);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/

        private bool NguoiDungExists(int id)
        {
            return _context.NguoiDungs.Any(e => e.MaNd == id);
        }
    }
}
