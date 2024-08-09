using EcomerceMVC.Data;

namespace EcomerceMVC.ViewModels
{
	public class DonHangViewModel
	{
		public int MaHd { get; set; }
		public DateTime NgayDat { get; set; }

		public string? HoTen { get; set; }

		public string DiaChi { get; set; }

		public string CachThanhToan { get; set; }

		public string CachVanChuyen { get; set; }

		public int MaTrangThai { get; set; }

		public double TongTien { get; set; }
	}
}
