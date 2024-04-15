﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopXanh.Data;
using ShopXanh.Models;

namespace ShopXanh.Controllers
{
    public class NguoiDungsController : Controller
    {
        private readonly ShopXanhContext _context;
        public NguoiDungsController(ShopXanhContext context)
        {
            _context = context;
        }

        // GET: NguoiDungs
        public async Task<IActionResult> Index()
        {
            return View(await _context.NguoiDung.ToListAsync());
        }

        // GET: NguoiDungs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDung
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            return View(nguoiDung);
        }

        // GET: NguoiDungs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NguoiDungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Address,Password,Phone,Role")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nguoiDung);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nguoiDung);
        }

        // GET: NguoiDungs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDung.FindAsync(id);
            if (nguoiDung == null)
            {
                return NotFound();
            }
            return View(nguoiDung);
        }

        // POST: NguoiDungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Address,Password,Phone,Role")] NguoiDung nguoiDung)
        {
            if (id != nguoiDung.Id)
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
                    if (!NguoiDungExists(nguoiDung.Id))
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

        // GET: NguoiDungs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDung
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            return View(nguoiDung);
        }

        // POST: NguoiDungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nguoiDung = await _context.NguoiDung.FindAsync(id);
            if (nguoiDung != null)
            {
                _context.NguoiDung.Remove(nguoiDung);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NguoiDungExists(int id)
        {
            return _context.NguoiDung.Any(e => e.Id == id);
        }
        private bool NguoiDungExists(string email,string password)
        {
            return _context.NguoiDung.Any(e => e.Email == email && e.Password == password);
        }
        private bool NguoiDungExists(string email)
        {
            return _context.NguoiDung.Any(e => e.Email == email);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(NguoiDung model)
        {
            if (ModelState.IsValid)
            {
                var nguoiDung = _context.NguoiDung.FirstOrDefaultAsync(e=>(e.Email == model.Email&&e.Password== model.Password));
                if (nguoiDung.Result != null)
                {
                    model.Id = nguoiDung.Result.Id;
                    model.Name = nguoiDung.Result.Name;
                    HttpContext.Session.SetInt32("UserId", model.Id);
                    HttpContext.Session.SetString("UserName", model.Name);
                    // Đăng nhập thành công, chuyển hướng đến trang chính
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Email hoặc password không hợp lệ!";
                    // Đăng nhập không thành công, hiển thị thông báo lỗi
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    return View(model);
                }
            }
            else
            {
                ViewBag.Error = "Dữ liệu không hợp lệ!";
                // Dữ liệu không hợp lệ, hiển thị form đăng nhập lại với các thông báo lỗi
                return View(model);
            }
        }
        public IActionResult Logout()
        {
            if (HttpContext.Session.Keys.Contains("UserId"))
            {
                HttpContext.Session.Remove("UserId");
                HttpContext.Session.Remove("UserName");
                return RedirectToAction("Login", "NguoiDungs");
            }
            
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,Name,Email,Address,Password,Phone,Role")] NguoiDung nguoiDung)
        {
            if (!ModelState.IsValid)
            {
                return View(nguoiDung);
            }
            if (NguoiDungExists(nguoiDung.Email))
            {
                ViewBag.Error = "Email Exists";
                return View(nguoiDung);
            }
            nguoiDung.Role = "User";
            _context.Add(nguoiDung);
            await _context.SaveChangesAsync();
            return RedirectToAction("Login");
            
        }

        [HttpGet]
        public async Task<IActionResult> ThongTin()
        {
            if (!HttpContext.Session.Keys.Contains("UserId"))
            {
                return NotFound();
            }
            var id = HttpContext.Session.GetInt32("UserId");
            var nguoiDung = await _context.NguoiDung
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nguoiDung == null)
            {
                return NotFound();
            }
            return View(nguoiDung);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThongTin([Bind("Id,Name,Email,Address,Password,Phone,Role")] NguoiDung nguoiDung)
        {

            if (!HttpContext.Session.Keys.Contains("UserId"))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nguoiDung);
                    await _context.SaveChangesAsync();
                    HttpContext.Session.SetString("UserName",nguoiDung.Name);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NguoiDungExists(nguoiDung.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ThongTin));
            }
            return View(nguoiDung);
        }
    }
}
