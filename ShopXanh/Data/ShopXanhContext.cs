using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopXanh.Models;

namespace ShopXanh.Data
{
    public class ShopXanhContext : DbContext
    {
        public ShopXanhContext (DbContextOptions<ShopXanhContext> options)
            : base(options)
        {
        }

        public DbSet<ShopXanh.Models.LoaiSanPham> LoaiSanPham { get; set; } = default!;
        public DbSet<ShopXanh.Models.NguoiDung> NguoiDung { get; set; } = default!;
        public DbSet<ShopXanh.Models.SanPham> SanPham { get; set; } = default!;
        public DbSet<ShopXanh.Models.Cart> Cart { get; set; } = default!;
        public DbSet<ShopXanh.Models.HoaDon> HoaDon { get; set; } = default!;
        public DbSet<ShopXanh.Models.ChiTietHoaDon> ChiTietHoaDon { get; set; } = default!;
    }
}
