using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using asm1.Entities;
using Microsoft.Extensions.Hosting;
using asm1.Models;

namespace asm1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/{controller}/{action}")]
    [Authentication]
    public class LoaiDienThoaisController : Controller
    {
        private readonly AspmvcH2storeContext _context;

        public LoaiDienThoaisController(AspmvcH2storeContext context)
        {
            _context = context;
        }

        // GET: Admin/LoaiDienThoais
        public async Task<IActionResult> Index()
        {
            return View(await _context.LoaiDienThoais.ToListAsync());
        }

        // GET: Admin/LoaiDienThoais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiDienThoai = await _context.LoaiDienThoais
                .FirstOrDefaultAsync(m => m.MaLoaiDt == id);
            if (loaiDienThoai == null)
            {
                return NotFound();
            }

            return View(loaiDienThoai);
        }

        // GET: Admin/LoaiDienThoais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiDienThoais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLoaiDt,TenLoaiDt")] LoaiDienThoai loaiDienThoai)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaiDienThoai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiDienThoai);
        }

        // GET: Admin/LoaiDienThoais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiDienThoai = await _context.LoaiDienThoais.FindAsync(id);
            if (loaiDienThoai == null)
            {
                return NotFound();
            }
            return View(loaiDienThoai);
        }

        // POST: Admin/LoaiDienThoais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaLoaiDt,TenLoaiDt")] LoaiDienThoai loaiDienThoai)
        {
            if (id != loaiDienThoai.MaLoaiDt)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiDienThoai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiDienThoaiExists(loaiDienThoai.MaLoaiDt))
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
            return View(loaiDienThoai);
        }

        public IActionResult Delete(int id)
        {
            var loaidienthoai = _context.LoaiDienThoais.Find(id);
            if (loaidienthoai == null)
            {
                return RedirectToAction("Index", "LoaiDienThoais");
            }

            _context.LoaiDienThoais.Remove(loaidienthoai);
            _context.SaveChanges();

            return RedirectToAction("Index", "LoaiDienThoais");
        }

        private bool LoaiDienThoaiExists(int id)
        {
            return _context.LoaiDienThoais.Any(e => e.MaLoaiDt == id);
        }
    }
}
