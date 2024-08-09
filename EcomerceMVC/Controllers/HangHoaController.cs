using EcomerceMVC.Helpers;
using EcomerceMVC.IRepositorys;
using EcomerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EcomerceMVC.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly IHangHoaRepository _hanghoaRpo;

        public HangHoaController(IHangHoaRepository hanghoaRpo) 
        {
            _hanghoaRpo = hanghoaRpo;
        }
        public async Task<IActionResult> Index(int? MaLoai, int PageNumber = 1)
        {
            var result = await _hanghoaRpo.GetHangHoa(MaLoai);
            if (PageNumber < 1)
            {
                PageNumber = 1;
            }
            int Pagesize = 9;
            var list = PaginatedList<HangHoaViewModel>.CreateAsync(result, PageNumber, Pagesize);
            return View(list);
        }
        public async Task<IActionResult> Search(string? Query)
        {
            var result = await _hanghoaRpo.SearchHangHoa(Query);
            return View(result);
        }

        public async Task<IActionResult> Detail(int Id)
        {
            var result = await _hanghoaRpo.GetDetailHangHoa(Id);
            if (result == null)
            {
                TempData["Message"] = "Xin lỗi. Không thấy sản phẩm đó";
                return Redirect("/404");
            }
            return View(result);
        }
    }
}
