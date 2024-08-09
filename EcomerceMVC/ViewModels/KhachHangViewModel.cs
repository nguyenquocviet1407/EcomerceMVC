using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EcomerceMVC.ViewModels
{
    public class KhachHangViewModel
    {
        [Key]
        [Display(Name ="Tài khoản")]
        [Required(ErrorMessage ="Bạn chưa nhập tài khoản!")]
        [MaxLength(20,ErrorMessage ="Tối đa 20 ký tự!")]
        public string MaKh { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu!")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự!")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Bạn chưa nhập họ tên!")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự!")]
        public string HoTen { get; set; }

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime? NgaySinh { get; set; }

        [Display(Name = "Giới tính")]
        public bool GioiTinh { get; set; } = true;

        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ!")]
        [MaxLength(60, ErrorMessage = "Tối đa 60 ký tự!")]
        public string DiaChi { get; set; }

        [Display(Name = "Số điện thoại")]
        [MaxLength(24, ErrorMessage = "Tối đa 24 ký tự!")]
		[Required(ErrorMessage = "Bạn chưa nhập số điện thoại!")]
		[RegularExpression(@"0[39875]\d{8}",ErrorMessage ="Chưa đúng định dạng số điện thoại Việt Nam")]
        public string DienThoai { get; set; }

        [Display(Name = "Email")]
		[Required(ErrorMessage = "Bạn chưa nhập email!")]
		[EmailAddress(ErrorMessage ="Chưa đúng định dạng email!")]
        public string Email { get; set; }

		//public string? Hinh { get; set; }
		public bool HieuLuc { get; set; } 
	}
}
