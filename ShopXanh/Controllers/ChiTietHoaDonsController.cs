using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopXanh.Data;
using ShopXanh.Models;

namespace ShopXanh.Controllers
{
    public class ChiTietHoaDonsController : Controller
    {
        private readonly ShopXanhContext _context;

        public ChiTietHoaDonsController(ShopXanhContext context)
        {
            _context = context;
        }

        // GET: ChiTietHoaDons
        public async Task<IActionResult> Index()
        {
            var shopXanhContext = _context.ChiTietHoaDon.Include(c => c.hoaDon).Include(c => c.sanPham);
            return View(await shopXanhContext.ToListAsync());
        }

        // GET: ChiTietHoaDons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietHoaDon = await _context.ChiTietHoaDon
                .Include(c => c.hoaDon)
                .Include(c => c.sanPham)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (chiTietHoaDon == null)
            {
                return NotFound();
            }

            return View(chiTietHoaDon);
        }

        // GET: ChiTietHoaDons/Create
        public IActionResult Create()
        {
            ViewData["HoaDonID"] = new SelectList(_context.HoaDon, "Id", "Id");
            ViewData["SanPhamId"] = new SelectList(_context.SanPham, "Id", "Id");
            return View();
        }

        // POST: ChiTietHoaDons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Quantity,Total,HoaDonID,SanPhamId")] ChiTietHoaDon chiTietHoaDon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiTietHoaDon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HoaDonID"] = new SelectList(_context.HoaDon, "Id", "Id", chiTietHoaDon.HoaDonID);
            ViewData["SanPhamId"] = new SelectList(_context.SanPham, "Id", "Id", chiTietHoaDon.SanPhamId);
            return View(chiTietHoaDon);
        }

        // GET: ChiTietHoaDons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietHoaDon = await _context.ChiTietHoaDon.FindAsync(id);
            if (chiTietHoaDon == null)
            {
                return NotFound();
            }
            ViewData["HoaDonID"] = new SelectList(_context.HoaDon, "Id", "Id", chiTietHoaDon.HoaDonID);
            ViewData["SanPhamId"] = new SelectList(_context.SanPham, "Id", "Id", chiTietHoaDon.SanPhamId);
            return View(chiTietHoaDon);
        }

        // POST: ChiTietHoaDons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Quantity,Total,HoaDonID,SanPhamId")] ChiTietHoaDon chiTietHoaDon)
        {
            if (id != chiTietHoaDon.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTietHoaDon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietHoaDonExists(chiTietHoaDon.ID))
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
            ViewData["HoaDonID"] = new SelectList(_context.HoaDon, "Id", "Id", chiTietHoaDon.HoaDonID);
            ViewData["SanPhamId"] = new SelectList(_context.SanPham, "Id", "Id", chiTietHoaDon.SanPhamId);
            return View(chiTietHoaDon);
        }

        // GET: ChiTietHoaDons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietHoaDon = await _context.ChiTietHoaDon
                .Include(c => c.hoaDon)
                .Include(c => c.sanPham)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (chiTietHoaDon == null)
            {
                return NotFound();
            }

            return View(chiTietHoaDon);
        }

        // POST: ChiTietHoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chiTietHoaDon = await _context.ChiTietHoaDon.FindAsync(id);
            if (chiTietHoaDon != null)
            {
                _context.ChiTietHoaDon.Remove(chiTietHoaDon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChiTietHoaDonExists(int id)
        {
            return _context.ChiTietHoaDon.Any(e => e.ID == id);
        }
    }
}
