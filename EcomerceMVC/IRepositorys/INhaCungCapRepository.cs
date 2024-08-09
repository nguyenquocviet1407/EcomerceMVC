using EcomerceMVC.ViewModels;

namespace EcomerceMVC.IRepositorys
{
	public interface INhaCungCapRepository
	{
		Task<List<NhapNhaCungCapViewModel>> GetAllNhaCungCap();
		Task<NhapNhaCungCapViewModel?> GetNhaCungCapById(string mancc);
		Task<NhapNhaCungCapViewModel?> NhapNhaCungCap(NhapNhaCungCapViewModel model);
		Task<int?> XoaNhaCungCap(string mancc);
		Task<int?> CapNhatNhaCungCap(NhapNhaCungCapViewModel model);
	}
}
