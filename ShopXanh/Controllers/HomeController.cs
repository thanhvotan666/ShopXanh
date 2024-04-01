using Microsoft.AspNetCore.Mvc;
using ShopXanh.Data;
using ShopXanh.Models;
using System.Diagnostics;

namespace ShopXanh.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShopXanhContext _context;

        public HomeController(ShopXanhContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.SanPham.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult CuaHang()
        {
            return View();
        }
        public IActionResult YeuThich()
        {
            return View();
        }
        public IActionResult GioiThieu()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult Blog()
        {
            return View();
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
