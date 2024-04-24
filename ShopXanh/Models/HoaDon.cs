using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopXanh.Models
{
    public class HoaDon
    {
        [Key]
        public int Id { get; set; }
        public decimal TotalPayment { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("NguoiDung")]
        public int NguoiDungId { get; set; }
        public virtual NguoiDung? NguoiDung { get; set; }
    }
}
