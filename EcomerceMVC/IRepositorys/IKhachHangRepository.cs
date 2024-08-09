using EcomerceMVC.Data;
using EcomerceMVC.ViewModels;
using System.Net.Http.Headers;

namespace EcomerceMVC.IRepositorys
{
	public interface IKhachHangRepository
	{
		Task<int?> RegisterKH(KhachHangViewModel model, IFormFile? hinh);
		Task<KhachHangViewModel?> CheckDangnhap(DangNhapViewModel model);
		Task<KhachHang> GetKhachHang(string makh);
		Task<string> GetRole(string makh);

	}
}
