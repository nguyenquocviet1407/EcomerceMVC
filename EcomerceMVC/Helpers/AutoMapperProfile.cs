using AutoMapper;
using EcomerceMVC.Controllers;
using EcomerceMVC.Data;
using EcomerceMVC.ViewModels;

namespace EcomerceMVC.Helpers
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<KhachHang, KhachHangViewModel>().ReverseMap();
			CreateMap<Loai, LoaiHangHoaViewModel>().ReverseMap();
			CreateMap<NhaCungCap, NhaCungCapViewModel>().ReverseMap();
			CreateMap<HangHoa, NhapHangHoaViewModel>().ReverseMap();
			CreateMap<HangHoa, HangHoaModel>().ReverseMap();
			CreateMap<HangHoa, CapNhatHangHoaModel>().ReverseMap();
			CreateMap<NhaCungCap, NhapNhaCungCapViewModel>().ReverseMap();
		}
	}
}
