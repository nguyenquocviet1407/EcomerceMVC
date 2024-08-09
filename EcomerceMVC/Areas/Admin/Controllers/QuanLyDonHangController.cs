using EcomerceMVC.IRepositorys;
using Microsoft.AspNetCore.Mvc;

namespace EcomerceMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuanLyDonHangController : Controller
    {
		private readonly IHoaDonRepository _HoaDonRpo;

		public QuanLyDonHangController(IHoaDonRepository HoaDonRpo) 
        {
			_HoaDonRpo = HoaDonRpo;

		}
        public IActionResult Index()
        {
            return View();
        }
		
		public async Task<IActionResult> LoadHoaDon()
		{
            var result = await _HoaDonRpo.GetHoaDonAll();
			return View();
		}
	}
}
