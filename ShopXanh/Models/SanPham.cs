using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopXanh.Models
{
    public class SanPham
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }

        [ForeignKey("LoaiSanPham")]
        public int LoaiSanPhamId { get; set; }

        public virtual LoaiSanPham? loaiSanPham { get; set; }
    }
}
