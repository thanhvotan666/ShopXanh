using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopXanh.Models
{
    public class ChiTietHoaDon
    {
        [Key]
        public int ID { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        [ForeignKey("HoaDon")]
        public int HoaDonID { get; set; }
        public virtual HoaDon? hoaDon { get; set; }
        [ForeignKey("SanPham")]
        public int SanPhamId { get; set; }
        public virtual SanPham? sanPham { get; set; }

    }
}
