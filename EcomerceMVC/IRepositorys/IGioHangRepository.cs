using EcomerceMVC.Data;
using EcomerceMVC.ViewModels;

namespace EcomerceMVC.IRepositorys
{
    public interface IGioHangRepository
    {
        Task<HangHoaViewModel> GetHangHoa(int id,int quantity);
        Task<int?> CreateHoaDon(HoaDon model, List<ChiTietGioHangViewModel> giohangs);
        Task<List<ChiTietGioHangViewModel?>> GetGioHang(string makh);
        Task<int?> ThemHhVaoGioHang(int mahh, int soluong, string makh);
        Task<int?> GiamSlHhGioHang(int mahh, int soluong, string makh);
        Task<int?> XoaHhGioHang(int mahh, string makh);
    }
}
