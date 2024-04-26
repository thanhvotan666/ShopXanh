using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            return View(_context.SanPham.Include(s => s.loaiSanPham).Take(8).ToList());
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
			return View(_context.SanPham.Include(s => s.loaiSanPham).Where(e=>cookies.Contains(e.Id)).ToList());
        }
        public IActionResult GioiThieu()
        {
            return View();
        }
		[HttpGet]
		public IActionResult Cart(int? id,string? error)
        {
			if (id.HasValue)
			{
				Response.Cookies.Delete($"Cart{id}");
			}
			List<int> cookies = new List<int>();
			foreach (var cookie in Request.Cookies)
			{ 
				if (cookie.Key.Contains("Cart"))
				{
                    int cookieId = int.Parse(cookie.Key.Substring(4));
					if (!id.HasValue || id != cookieId)
					{
						cookies.Add(cookieId);
                    }
				}
			}
            var sanPham = _context.SanPham.Include(s => s.loaiSanPham).Where(e => cookies.Contains(e.Id)).ToList();
			return View(sanPham);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Cart([Bind("Id,TotalPayment,Address,Date,NguoiDungId")] HoaDon hoaDon)
		{
			if (ModelState.IsValid)
			{
                // Thêm hóa đơn
                if (HttpContext.Session.Keys.Contains("UserId"))
                {
                    hoaDon.NguoiDungId = (int)HttpContext.Session.GetInt32("UserId");
                }
                else
                {
                    hoaDon.NguoiDungId = 0;
                }
                hoaDon.Date = DateTime.Now;
				_context.Add(hoaDon);
                // Lưu hóa đơn
				await _context.SaveChangesAsync();
				Dictionary<string,string> cartCookies = new Dictionary<string, string>();
				// Dò cookies
				foreach (var cookie in Request.Cookies)
				{
					if (cookie.Key.Contains("Cart"))
					{
						cartCookies.Add(cookie.Key,cookie.Value);
					}
				}
				// Thêm các chi tiết hóa đơn
				foreach (var cookie in cartCookies)
                {
					// Kiểm tra sản phẩm tồn tại hay không
					var cookieId = int.Parse(cookie.Key.Substring(4));
					var sanPhamCart = _context.SanPham.Include(s => s.loaiSanPham).FirstOrDefault(e => e.Id == cookieId);
					if (sanPhamCart != null)
					{
						// Thêm chi tiết
						ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon();

						chiTietHoaDon.Quantity = int.Parse(cookie.Value);
						chiTietHoaDon.Total = chiTietHoaDon.Quantity * (int)sanPhamCart?.Price;
						chiTietHoaDon.HoaDonID = hoaDon.Id;
						chiTietHoaDon.SanPhamId = cookieId;

						_context.Add(chiTietHoaDon);
					}
				}
				// Lưu chi tiết
				await _context.SaveChangesAsync();
				// Xóa cookies
				foreach (var cookie in cartCookies)
				{
					Response.Cookies.Delete(cookie.Key);
				}
				return RedirectToAction(nameof(Index), "Home");
            }
            else
            {
				ViewBag.Error = "Vui lòng nhập đầy đủ thông tin";
			}

			List<int> cookies = new List<int>();
			foreach (var cookie in Request.Cookies)
			{
				if (cookie.Key.Contains("Cart"))
				{
					int cookieId = int.Parse(cookie.Key.Substring(4));
					cookies.Add(cookieId);
				}
			}
			var sanPham = await _context.SanPham.Include(s => s.loaiSanPham).Where(e => cookies.Contains(e.Id)).ToListAsync();
			return View(sanPham);

		}
		public IActionResult Blog()
        {
            return View();
        }

		public IActionResult ChinhSach()
		{
			return View();
		}
		public IActionResult LienHe()
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
