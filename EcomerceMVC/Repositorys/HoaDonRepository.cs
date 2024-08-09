using EcomerceMVC.Data;
using EcomerceMVC.IRepositorys;
using EcomerceMVC.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EcomerceMVC.Repositorys
{
	public class HoaDonRepository : IHoaDonRepository
	{
		private readonly Hshop2023Context _context;

		public HoaDonRepository(Hshop2023Context context) 
		{
			_context = context;
		}

		public async Task<List<DonHangViewModel>> GetHoaDonAll()
		{
			var hoadons = await _context.HoaDons.Include(x => x.ChiTietHds).Include(x => x.MaTrangThaiNavigation).Include(x => x.MaKhNavigation).ToListAsync();
			return null;
		}
	}
}
