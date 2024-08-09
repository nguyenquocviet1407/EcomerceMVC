using EcomerceMVC.IRepositorys;
using EcomerceMVC.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EcomerceMVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class DanhMucHangHoaController : Controller
	{
        private readonly IHangHoaRepository _hanghoaRpo;

        public DanhMucHangHoaController(IHangHoaRepository hanghoaRpo) 
		{
            _hanghoaRpo = hanghoaRpo;

        }
		public  async Task<IActionResult> Index()
		{		
			var Loaihanghoa = await _hanghoaRpo.GetLoaiHangHoa();
			var Nhacungcap = await _hanghoaRpo.GetNhaCungCap();

			ViewBag.LoaiHangHoa = new SelectList(Loaihanghoa, "MaLoai", "TenLoai");
			ViewBag.Nhacungcap = new SelectList(Nhacungcap, "MaNcc", "TenCongTy");

			//ViewBag.LoaiHangHoa = Loaihanghoa;
			return View();
		}
		public async Task<IActionResult> LoadDMHangHoa()
		{
			var result = await _hanghoaRpo.GetHangHoaAll();
			return Json(result);
		}

		[HttpPost]
		public async Task<IActionResult> NhapHangHoa(NhapHangHoaViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _hanghoaRpo.CreateHangHoa(model);
				if (result == null)
				{
					return Json(new { success = false });

				}
				return Json(new { success = true, data = result });
			}
			return RedirectToAction("Index");
		}
		[HttpGet]
		public IActionResult LoadDMHangHoaByID(int MaHh)
		{
			return View();
		}
		[HttpDelete]
		public async Task<IActionResult> DeleteHangHoa(int MaHh)
		{
			var result = await _hanghoaRpo.DeleteHangHoa(MaHh);
			if (result == null)
			{
				return Json(new { success = false });
			}
			return Json(new { success = true });
		}
		[HttpGet]
		public async Task<IActionResult> UpdateHangHoa(int MaHh)
		{
			var result = await _hanghoaRpo.GetHangHoaById(MaHh);

			var Loaihanghoa = await _hanghoaRpo.GetLoaiHangHoa();
			var Nhacungcap = await _hanghoaRpo.GetNhaCungCap();

			ViewBag.LoaiHangHoa = new SelectList(Loaihanghoa, "MaLoai", "TenLoai");
			ViewBag.Nhacungcap = new SelectList(Nhacungcap, "MaNcc", "TenCongTy");
			return View(result);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateHangHoa(CapNhatHangHoaModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _hanghoaRpo.UpdateHangHoa(model);
				if (result == null)
				{
					return Json(new { success = false });
				}
				return Json(new { success = true });
			}
			return View(model);
		}

	}
}
