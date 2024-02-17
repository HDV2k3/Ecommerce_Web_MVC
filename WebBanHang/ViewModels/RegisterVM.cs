using System.ComponentModel.DataAnnotations;

namespace WebBanHang.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Tên Đăng Nhập")]
        [Required(ErrorMessage="*")]
        [MaxLength(20,ErrorMessage="Tối Đa 20 Ký Tự")]
        public required string MaKh { get; set; }

        [Display(Name ="Mật Khẩu")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]   
        public required string MatKhau { get; set; }

        [Display(Name ="Họ Tên")]
        [MaxLength(50, ErrorMessage = "Tối Đa 50 Ký Tự")]
        public required string HoTen { get; set; }

        public bool GioiTinh { get; set; } = true;

        [Display(Name ="Ngày Sinh")]
        [DataType(DataType.Date)]
        public DateTime? NgaySinh { get; set; }

        [MaxLength(60, ErrorMessage = "Tối Đa 60 Ký Tự")]
        [Display(Name = "Địa Chỉ")]
        public required string DiaChi { get; set; }

        [Display(Name = "Điện Thoại")]
        [Required(ErrorMessage = "*")]
        [MaxLength(10, ErrorMessage = "Tối Đa 10 Ký Tự")]
        [RegularExpression(@"0[312]\d{8}",ErrorMessage =("Chưa Đúng Định Dạng"))]
        public required string DienThoai { get; set; }

   
        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage =("Chưa Đúng Định Dạng Email"))]            
        public required string Email { get; set; }

        [Display(Name = "Hình")]
        public string? Hinh { get; set; }

    }
}
