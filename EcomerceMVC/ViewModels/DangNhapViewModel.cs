using System.ComponentModel.DataAnnotations;

namespace EcomerceMVC.ViewModels
{
	public class DangNhapViewModel
	{
		[Display(Name = "Tài Khoản")]
		[Required(ErrorMessage = "Bạn chưa nhập tài khoản!")]
		[MaxLength(20, ErrorMessage = "Tối đa 20 ký tự!")]
		public string TaiKhoan { get; set; }

		[Display(Name = "Mật khẩu")]
		[Required(ErrorMessage = "Bạn chưa nhập mật khẩu!")]
		[MaxLength(50, ErrorMessage = "Tối đa 50 ký tự!")]
		[DataType(DataType.Password)]
		public string MatKhau { get; set; }
		
	}
}
