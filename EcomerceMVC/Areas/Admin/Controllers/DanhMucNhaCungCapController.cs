using EcomerceMVC.IRepositorys;
using EcomerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EcomerceMVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class DanhMucNhaCungCapController : Controller
	{
		private readonly INhaCungCapRepository _NhaCCRpo;

		public DanhMucNhaCungCapController(INhaCungCapRepository NhaCCRpo)
		{
			_NhaCCRpo = NhaCCRpo;
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public async Task<IActionResult> LoadDMNhaCC()
		{
			var result = await _NhaCCRpo.GetAllNhaCungCap();
			return Json(result);
		}

		[HttpPost]
		public async Task<IActionResult> NhapNhaCC(NhapNhaCungCapViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _NhaCCRpo.NhapNhaCungCap(model);
				if (result == null)
				{
					return Json(new { success = false });
				}
				return Json(new { success = true, data = result });
			}
			return View(model);
		}

		[HttpDelete]
		public async Task<IActionResult> XoaNhaCC(string MaNcc)
		{
			var result = await _NhaCCRpo.XoaNhaCungCap(MaNcc);
			if (result == null)
			{
				return Json(new { success = false });
			}
			return Json(new { success = true});
		}

		[HttpGet]
		public async Task<IActionResult> UpdateNhaCungCap(string MaNcc)
		{
			var result = await _NhaCCRpo.GetNhaCungCapById(MaNcc);
			return View(result);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateNhaCungCap(NhapNhaCungCapViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _NhaCCRpo.CapNhatNhaCungCap(model);
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
