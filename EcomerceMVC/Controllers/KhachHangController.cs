using EcomerceMVC.Helpers;
using EcomerceMVC.IRepositorys;
using EcomerceMVC.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcomerceMVC.Controllers
{
	public class KhachHangController : Controller
	{
		private readonly IKhachHangRepository _khachhangRpo;

		public KhachHangController(IKhachHangRepository khachhangRpo)
		{
			_khachhangRpo = khachhangRpo;

		}
		[HttpGet]
		public IActionResult DangKy()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> DangKy(KhachHangViewModel model, IFormFile? Hinh)
		{
			// kiểm tra lỗi ở field nào ?
			//if (!ModelState.IsValid)
			//{
			//	foreach (var entry in ModelState)
			//	{
			//		if (entry.Value.Errors.Count > 0)
			//		{
			//			var a = $"Field: {entry.Key}, Error: {entry.Value.Errors.First().ErrorMessage}";
			//		}
			//	}
			//}
			if (ModelState.IsValid)
			{
				try
				{
					var result = await _khachhangRpo.RegisterKH(model, Hinh);
					if (result == null)
					{
						return View();
					}
					return RedirectToAction("Index", "HangHoa");
				}
				catch (Exception ex)
				{
					var mess = $"{ex.Message} shh";
				}
			}
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> DangNhap(string? returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> DangNhap(DangNhapViewModel model, string? returnUrl)
		{
			// Url trang yêu  
			ViewBag.ReturnUrl = returnUrl;
			if (ModelState.IsValid)
			{
				var khachhang = await _khachhangRpo.CheckDangnhap(model);
				if (khachhang == null)
				{
					ViewBag.Message = "Thông tin đăng nhập không đúng";
					return View();
				}
				if (khachhang.HieuLuc == false)
				{
					ViewBag.Message = "Tài khoản của bạn đã bị khóa. Vui lòng liên hệ Admin";
					return View();
				}

				string role = await _khachhangRpo.GetRole(khachhang.MaKh);
				// tạo Claim
				var claims = new List<Claim> {
					new Claim(ClaimTypes.Email,khachhang.Email),
					new Claim(ClaimTypes.Name,khachhang.HoTen),
					new Claim(MySetting.Claim_MaKH,khachhang.MaKh),
					new Claim(ClaimTypes.Role, role),
				};

				var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
				var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

				// nếu đăng nhập thành công thì chuyển đến trang yêu cầu trước đó 
				if (role == "CUSTOMER")
				{
					if (Url.IsLocalUrl(returnUrl))
					{
						return Redirect(returnUrl);
					}
					else
					{
						return Redirect("/HangHoa/Index");
					}
				}
				else if  (role == "ADMIN") 
				{
					return Redirect("~/Admin/DanhMucHangHoa/Index");
				}	
				else
				{
					ViewBag.Message = "Tài khoản của bạn chưa có quyền. Vui lòng liên hệ Admin";
					return View();
				}	
			}
			else
			{
				return View();
			}

		}
		[Authorize]
		public async Task<IActionResult> DangXuat()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return Redirect("/HangHoa/Index");
		}

	}
}
