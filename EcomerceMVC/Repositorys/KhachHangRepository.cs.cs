using AutoMapper;
using EcomerceMVC.Data;
using EcomerceMVC.Helpers;
using EcomerceMVC.IRepositorys;
using EcomerceMVC.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EcomerceMVC.Repositorys
{
	public class KhachHangRepository : IKhachHangRepository
	{
		private readonly Hshop2023Context _context;
		private readonly IMapper _mapper;

		public KhachHangRepository(Hshop2023Context context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<KhachHangViewModel?> CheckDangnhap(DangNhapViewModel model)
		{
			
			var khachhang = await _context.KhachHangs.SingleOrDefaultAsync(x => x.MaKh == model.TaiKhoan);
			if (khachhang == null)
			{
				return null;
			}
			string makhau = MyUtil.ToMd5Hash(model.MatKhau, khachhang.RandomKey);
			if (khachhang.MatKhau != makhau)
			{
				return null;
			}
			return _mapper.Map<KhachHangViewModel>(khachhang);
		}

		public async Task<KhachHang?> GetKhachHang(string makh)
		{
			var result = await _context.KhachHangs.SingleOrDefaultAsync(x => x.MaKh == makh);
			if (result == null)
			{
				return null;
			}
			return result;
		}

		public async Task<string?> GetRole(string makh)
		{
			var result = await _context.UserRoles.Include(x => x.Role).SingleOrDefaultAsync(x => x.UserId == makh);
			if (result == null)
			{
				return null;
			} 
			return result.Role.RoleName;
				
		}

		public async Task<int?> RegisterKH(KhachHangViewModel model, IFormFile? hinh)
		{
			
			var khachhang = _mapper.Map<KhachHang>(model);
			khachhang.RandomKey = MyUtil.GenerateRamdomKey();
			khachhang.MatKhau = MyUtil.ToMd5Hash(model.MatKhau,khachhang.RandomKey);
			khachhang.HieuLuc = true;
			khachhang.VaiTro = 0;

			if (hinh != null) 
			{
				khachhang.Hinh = MyUtil.UpLoadHinh(hinh, "KhachHang");
			}
			await _context.AddAsync(khachhang);
		    int num = await _context.SaveChangesAsync();

			return num;
		}
	}
}
