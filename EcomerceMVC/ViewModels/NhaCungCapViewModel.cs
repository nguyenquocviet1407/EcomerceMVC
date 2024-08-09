using System.ComponentModel.DataAnnotations;

namespace EcomerceMVC.ViewModels
{
	public class NhaCungCapViewModel
	{
		public string MaNcc { get; set; }
		public string TenCongTy { get; set; }
	}
	public class NhapNhaCungCapViewModel
	{
		[Display(Name = "Mã nhà cung cấp")]
		[Required(ErrorMessage = "Bạn chưa nhập mã nhà cung cấp")]
		[MaxLength(50,ErrorMessage = "Tối đa 50 ký tự")]
		public string MaNcc { get; set; }

		[Display(Name = "Tên nhà cung cấp")] 
		[Required(ErrorMessage = "Tên nhà cung cấp")]
		[MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
		public string TenCongTy { get; set; }

		public IFormFile? FileHinh { get; set; }

		[Display(Name = "Logo")]
		public string? Logo { get; set; }

		[Display(Name = "Người liên lạc")]
		[MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
		public string? NguoiLienLac { get; set; }

		[Display(Name = "Email")]
		[Required(ErrorMessage = "Bạn chưa nhâp Email")]
		[MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
		[DataType(DataType.EmailAddress,ErrorMessage = "Bạn nhập sai định dạng")]
		public string Email { get; set; }

		[Display(Name = "Điện thoại")]
		[Required(ErrorMessage = "Bạn chưa nhập số điện thoại")]
		[RegularExpression(@"0[39875]\d{8}", ErrorMessage = "Chưa đúng định dạng số điện thoại Việt Nam")]
		public string DienThoai { get; set; }

		[Display(Name = "Đia chỉ")]
		[Required(ErrorMessage = "Bạn chưa nhập địa chỉ")]
		[MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
		public string DiaChi { get; set; }

		[Display(Name = "Mô tả")]
		public string? MoTa { get; set; }
	}

}
