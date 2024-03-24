using System.ComponentModel.DataAnnotations;

namespace ShopXanh.Models
{
    public class LoaiSanPham
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
