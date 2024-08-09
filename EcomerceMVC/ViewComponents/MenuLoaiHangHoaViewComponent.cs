using EcomerceMVC.Data;
using EcomerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace EcomerceMVC.ViewComponents
{
    public class MenuLoaiHangHoaViewComponent : ViewComponent
    {
        private readonly Hshop2023Context db;

        public MenuLoaiHangHoaViewComponent(Hshop2023Context context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Loais.Select(x => new MenuLoaiHangHoaModel
            {
                MaLoai = x.MaLoai, 
                TenLoai = x.TenLoai,
                SoLuong = x.HangHoas.Count,
            });
            return View("Defautl", data);
        }
    }
}
