using EcomerceMVC.Helpers;
using EcomerceMVC.IRepositorys;
using EcomerceMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcomerceMVC.ViewComponents
{
    public class GioHangPanelViewComponent : ViewComponent
    {
        private readonly IGioHangRepository _gioHangRpo;

        public GioHangPanelViewComponent(IGioHangRepository gioHangRpo) 
        {
            _gioHangRpo = gioHangRpo;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            int checkClaim = HttpContext.User.Claims.Count();
            string makh;
            if (checkClaim == 0)
            {
                makh = "";
            } 
            else
            {
                makh = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "MaKH").Value;
            }    
            var giohang = await _gioHangRpo.GetGioHang(makh);
            //var giohang = HttpContext.Session.Get<List<ChiTietGioHangViewModel>>(MySetting.SessionKey) ?? new List<ChiTietGioHangViewModel>();
            if (giohang == null)
            {
                return View("CartPanel", new GioHangViewModel
                {
                    SoLuong = 0,
                    Total = 0,
                });
            }
            return View("CartPanel", new GioHangViewModel
            {
                SoLuong = giohang.Sum(x => x.SoLuong),
                Total = giohang.Sum(x => x.ThanhTien),
            });
        }
    }
}
