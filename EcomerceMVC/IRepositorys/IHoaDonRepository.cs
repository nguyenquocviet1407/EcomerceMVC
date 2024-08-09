using EcomerceMVC.ViewModels;

namespace EcomerceMVC.IRepositorys
{
	public interface IHoaDonRepository
	{
		Task<List<DonHangViewModel>> GetHoaDonAll();
	}
}
