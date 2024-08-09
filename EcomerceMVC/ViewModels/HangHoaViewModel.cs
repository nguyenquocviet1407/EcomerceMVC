using EcomerceMVC.Data;
using System.ComponentModel.DataAnnotations;

namespace EcomerceMVC.ViewModels
{
    public class HangHoaViewModel
    {
        public int MaHH { get; set; }
        public string TenHH { get; set; }
        public string Hinh { get; set; }
        public double DonGia { get; set; }
        public string MoTaNgan { get; set; }
		public int MaLoai { get; set; }
		public string TenLoai { get;set; }
    }

    public class ChiTietHangHoaViewModel
    {
        public int MaHH { get; set; }
        public string TenHH { get; set; }
        public string Hinh { get; set; }
        public double DonGia { get; set; }
        public string MoTaNgan { get; set; }
        public string TenLoai { get; set; }
        public string ChiTiet { get; set; }
        public int DiemDanhGia { get; set; }
        public int SoLuongTon { get; set; }
    }
    public class HangHoaModel
    {
        public int MaHh { get; set; }
        public string TenHh { get; set; }
        public string TenAlias { get; set; }
        public int MaLoai { get; set; }
		public string TenLoai { get; set; }
        public string MoTaDonVi { get; set; }
        public double? DonGia { get; set; }
        public string Hinh { get; set; }
        public DateTime NgaySx { get; set; }
        public double GiamGia { get; set; }
        public int SoLanXem { get; set; }
        public string MoTa { get; set; }
		public string NhaCC { get; set; }

    }
	public class NhapHangHoaViewModel
	{
        [Display(Name ="Tên hàng hóa")]
        [Required(ErrorMessage ="Bạn chưa nhập tên hàng hóa")]
        [MaxLength(50,ErrorMessage ="Tối đa 50 ký tự")]
		public string TenHh { get; set; }

		[Display(Name = "Tên Alias")]
		[MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
		public string? TenAlias { get; set; }

		[Display(Name = "Loại hàng hóa")]
		[Required(ErrorMessage = "Bạn chưa chọn loại hàng hóa")]
		public string MaLoai { get; set; }

		[Display(Name = "Mô tả DV")]
		[MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
		public string? MoTaDonVi { get; set; }

		[Display(Name = "Đơn giá")]
		[Required(ErrorMessage = "Bạn chưa nhập đơn giá")]
		[RegularExpression("([0-9]+)", ErrorMessage = "Bạn nhập sai định dạng")]
		public double DonGia { get; set; }

		[Display(Name = "Ngày sản xuất")]
		[DataType(DataType.Date)]
		[Required(ErrorMessage = "Bạn chưa nhập ngày sản xuất")]
		public DateTime NgaySx { get; set; }

		//public double GiamGia { get; set; }
		//public int SoLanXem { get; set; }

		[Display(Name = "Mô tả hàng hóa")]
		public string? MoTa { get; set; }

		[Display(Name = "Nhà cung cấp")]
		[Required(ErrorMessage = "Bạn chưa chọn nhà cung cấp")]
		public string MaNcc { get; set; }

		
		public IFormFile? FileHinh { get; set; }

		[Display(Name = "Hình ảnh")]
		public string? Hinh { get; set; }

	}
	public class CapNhatHangHoaModel
	{
		public int MaHh { get; set; }

		[Display(Name = "Tên hàng hóa")]
		[Required(ErrorMessage = "Bạn chưa nhập tên hàng hóa")]
		[MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
		public string TenHh { get; set; }

		[Display(Name = "Tên Alias")]
		[MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
		public string? TenAlias { get; set; }

		[Display(Name = "Loại hàng hóa")]
		[Required(ErrorMessage = "Bạn chưa chọn loại hàng hóa")]
		public int MaLoai { get; set; }

		[Display(Name = "Mô tả DV")]
		[MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
		public string? MoTaDonVi { get; set; }

		[Display(Name = "Đơn giá")]
		[Required(ErrorMessage = "Bạn chưa nhập đơn giá")]
		[RegularExpression("([0-9]+)", ErrorMessage = "Bạn nhập sai định dạng")]
		public double DonGia { get; set; }

		[Display(Name = "Ngày sản xuất")]
		[DataType(DataType.Date)]
		[Required(ErrorMessage = "Bạn chưa nhập ngày sản xuất")]
		public DateTime NgaySx { get; set; }

		//public double GiamGia { get; set; }
		//public int SoLanXem { get; set; }

		[Display(Name = "Mô tả hàng hóa")]
		public string? MoTa { get; set; }

		[Display(Name = "Nhà cung cấp")]
		[Required(ErrorMessage = "Bạn chưa chọn nhà cung cấp")]
		public string MaNcc { get; set; }

		public IFormFile? FileHinh { get; set; }

		[Display(Name = "Hình ảnh")]
		public string? Hinh { get; set; }

	}
}
