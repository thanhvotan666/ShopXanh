using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopXanh.Data;

namespace ShopXanh.Controllers.Component
{
    public class CuaHangDS : ViewComponent
    {
        private readonly ShopXanhContext _context;

        public CuaHangDS(ShopXanhContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var loaiSanPham = await _context.LoaiSanPham.ToListAsync();
            return View("Default", loaiSanPham);
        }
    }
}
