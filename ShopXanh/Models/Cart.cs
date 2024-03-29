using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopXanh.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int Quantity {  get; set; }

        [ForeignKey("SanPham")]
        public int SanPhamId { get; set;}
        public virtual SanPham? sanPham { get; set;}
        [ForeignKey("NguoiDung")]
        public int NguoiDungId { get; set; }
        public virtual NguoiDung? nguoiDung { get; set;}

    }
}
