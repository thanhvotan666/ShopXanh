using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopXanh.Data;

namespace ShopXanh.Controllers.Component
{
    public class CuaHangSP : ViewComponent
    {
        private readonly ShopXanhContext _context;

        public CuaHangSP(ShopXanhContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int LoaiId,int Page)
        {
            if(LoaiId != 0)
            {
                return View("Default", await _context.SanPham.Where(s=>s.LoaiSanPhamId==LoaiId).Skip((Page-1)*8).Take(8).ToListAsync());
            }
            else
            {
                return View("Default", await _context.SanPham.Skip((Page - 1) * 8).Take(8).ToListAsync());
            }
        }
    }
}
