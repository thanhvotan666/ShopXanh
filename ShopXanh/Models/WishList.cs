using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopXanh.Models
{
    public class WishList
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("SanPham")]
        public int SanPhamId { get; set; }

        public virtual LoaiSanPham? sanPham { get; set; }
        [ForeignKey("NguoiDung")]
        public int NguoiDungId { get; set; }
        public virtual NguoiDung? nguoiDung { get; set; }
    }
}
