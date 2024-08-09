using EcomerceMVC.ViewModels;


namespace EcomerceMVC.IRepositorys
{
    public interface IHangHoaRepository
    {
       Task<List<HangHoaViewModel>> GetHangHoa(int? maloai);
       Task<List<HangHoaModel>> GetHangHoaAll();
       Task<List<HangHoaViewModel>> SearchHangHoa(string? query);
       Task<ChiTietHangHoaViewModel> GetDetailHangHoa(int id);
	   Task<CapNhatHangHoaModel> GetHangHoaById(int mahh);
	   Task<List<LoaiHangHoaViewModel>> GetLoaiHangHoa();
       Task<List<NhaCungCapViewModel>> GetNhaCungCap();
	   Task<HangHoaModel?> CreateHangHoa(NhapHangHoaViewModel model);
	   Task<int?> DeleteHangHoa(int mahh);
		Task<int?> UpdateHangHoa(CapNhatHangHoaModel model);

	}
}
