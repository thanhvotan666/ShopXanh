using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            //Xuất 8 sản phẩm
            return View(_context.SanPham.Take(8).ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public  IActionResult CuaHang(int? category,int? page)
        {
            ViewBag.Loai = 0;
            if (category != null)
            {
                ViewBag.Loai = category;
            }
            ViewBag.Page = 1;
            if (page != null)
            {
                ViewBag.Page = page;
            }            
            return View();
        }
        public IActionResult YeuThich(int? id)
        {
            
			List<int> cookies = new List<int>();
            foreach (var cookie in Request.Cookies)
            {
                if (cookie.Value.Equals("YeuThich"))
                {
					if (id.HasValue)
					{
						Response.Cookies.Delete($"SanPham{id}");
                        continue;
					}
					try
                    {
						cookies.Add(int.Parse(cookie.Key.Substring(7)));

					}
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
			return View(_context.SanPham.Where(e=>cookies.Contains(e.Id)).ToList());
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
