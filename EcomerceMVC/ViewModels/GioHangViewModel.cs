namespace EcomerceMVC.ViewModels
{
    public class GioHangViewModel
    {
        public int SoLuong {get;set;}
        public double Total {get;set;}
    }
    public class ChiTietGioHangViewModel
    {
        public int MaHH { get; set; }
        public string TenHH { get; set; }
        public string Hinh { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien => SoLuong * DonGia;

    }
}
