using EcomerceMVC.Data;
using EcomerceMVC.Helpers;
using EcomerceMVC.IRepositorys;
using EcomerceMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace EcomerceMVC.Controllers
{
	public class GioHangController : Controller
	{
		private readonly IGioHangRepository _giohangRpo;
		private readonly IKhachHangRepository _khachHangRpo;

		public GioHangController(IGioHangRepository giohangRpo, IKhachHangRepository khachHangRpo)
		{
			_giohangRpo = giohangRpo;
			_khachHangRpo = khachHangRpo;

		}

		//public List<ChiTietGioHangViewModel> GioHang => HttpContext.Session.Get<List<ChiTietGioHangViewModel>>(MySetting.SessionKey) ?? new List<ChiTietGioHangViewModel>();
		[Authorize]
		public async Task<IActionResult> Index()
		{
			string makh = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "MaKH").Value;
			var GioHang =  await _giohangRpo.GetGioHang(makh);
			if (GioHang == null)
			{
				GioHang = new List<ChiTietGioHangViewModel>();
				return View(GioHang);
			}
			return View(GioHang);
		}
		[Authorize]
		public async Task<IActionResult> AddToCart(int Id, int Quantity = 1)
		{
			string makh = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "MaKH").Value;
			var hanghoa = await _giohangRpo.ThemHhVaoGioHang(Id, Quantity, makh);
			if (hanghoa == null)
			{
				TempData["Message"] = "Xin lỗi. Không tìm thấy sản phẩm đó";
				return Redirect("/404");
			}
			return RedirectToAction("Index");

			//var giohang = GioHang;
			//var item = giohang.SingleOrDefault(x => x.MaHH == Id);
			//if (item == null)
			//{
			//	var hanghoa = await _giohangRpo.GetHangHoa(Id, Quantity);
			//	if (hanghoa == null)
			//	{
			//		TempData["Message"] = "Xin lỗi. Không tìm thấy sản phẩm đó";
			//		return Redirect("/404");
			//	}
			//	item = new ChiTietGioHangViewModel
			//	{
			//		MaHH = hanghoa.MaHH,
			//		TenHH = hanghoa.TenHH,
			//		DonGia = hanghoa.DonGia,
			//		Hinh = hanghoa.Hinh ?? string.Empty,
			//		SoLuong = Quantity
			//	};
			//	giohang.Add(item);
			//}
			//else
			//{
			//	item.SoLuong += Quantity;
			//}
			//HttpContext.Session.Set(MySetting.SessionKey, giohang);

			//return RedirectToAction("Index");
		}
        [HttpDelete]
        public async Task<IActionResult> XoaHHGioHang(int Id)
        {
            string makh = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "MaKH").Value;
            var result = await _giohangRpo.XoaHhGioHang(Id, makh);
            if (result == null)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
            //var giohang = GioHang;
            //var hanghoa = giohang.SingleOrDefault(x => x.MaHH == Id);
            //if (hanghoa != null)
            //{
            //	giohang.Remove(hanghoa);
            //	HttpContext.Session.Set(MySetting.SessionKey, giohang);
            //}
            //return RedirectToAction("Index");
        }

        [HttpDelete]
		public async Task<IActionResult> GiamSLHangHoa(int Id, int SoLuong)
		{
            string makh = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "MaKH").Value;
			var result = await _giohangRpo.GiamSlHhGioHang(Id, SoLuong, makh);
			if (result == null)
			{
				return Json(new { success = false });
			}
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> TangSLHangHoa(int Id, int SoLuong)
        {
            string makh = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "MaKH").Value;
            var result = await _giohangRpo.ThemHhVaoGioHang(Id, SoLuong, makh);
            if (result == null)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CheckOut()
        {
            string makh = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "MaKH").Value;
            var GioHang = await _giohangRpo.GetGioHang(makh);
            if (GioHang == null)
            {
                return Redirect("/HangHoa/Index");
            }
            return View(GioHang);
        }

        //[Authorize]
        //[HttpPost]
        //public async Task<IActionResult> CheckOut(DatHangViewModel model)
        //{

        //	if (ModelState.IsValid)
        //	{
        //		var KhachHangID = HttpContext.User.Claims.SingleOrDefault(x => x.Type == MySetting.Claim_MaKH)?.Value;

        //		var khachhang = new KhachHang();

        //		if (model.CungThongTin == true)
        //		{
        //			khachhang = await _khachHangRpo.GetKhachHang(KhachHangID);
        //		}
        //		var hoadon = new HoaDon
        //		{
        //			MaKh = KhachHangID,
        //			HoTen = model.HoTen ?? khachhang.HoTen,
        //			DiaChi = model.DiaChi ?? khachhang.DiaChi,
        //			SoDienThoai = model.SoDienThoai ?? khachhang.DienThoai,
        //			NgayDat = DateTime.Now,
        //			CachThanhToan = "COD",
        //			CachVanChuyen = "GRAB",
        //			MaTrangThai = 0,
        //			GhiChu = model.GhiChu
        //		};

        //		var resutl = await _giohangRpo.CreateHoaDon(hoadon, GioHang);
        //		if (resutl == null)
        //		{
        //			return Redirect("/GioHang/CheckOut");
        //		}
        //		// set giỏ hàng về rỗng
        //		HttpContext.Session.Set<List<ChiTietGioHangViewModel>>(MySetting.SessionKey, new List<ChiTietGioHangViewModel>());
        //		return View("Sussecc");
        //	}
        //	return View(GioHang);
        //}

        public IActionResult Sussecc()
		{
			return View();
		}
	}
}
