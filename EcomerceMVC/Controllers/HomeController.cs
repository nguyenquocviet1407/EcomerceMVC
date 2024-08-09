using EcomerceMVC.IRepositorys;
using EcomerceMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EcomerceMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly IHangHoaRepository _hangHoaRpo;

		public HomeController(ILogger<HomeController> logger , IHangHoaRepository hangHoaRpo)
        {
            _logger = logger;
			_hangHoaRpo = hangHoaRpo;

		}

        public async Task<IActionResult> Index()
        {

            ViewBag.HangHoas = await _hangHoaRpo.GetHangHoaAll();
            var result = await _hangHoaRpo.GetLoaiHangHoa();
			return View(result);
        }

        [Route("/404")]
        public IActionResult PageNotFound()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
