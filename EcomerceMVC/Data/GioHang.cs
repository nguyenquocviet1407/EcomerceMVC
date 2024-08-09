using System;
using System.Collections.Generic;

namespace EcomerceMVC.Data;

public partial class GioHang
{
    public int IdGh { get; set; }

    public string? MaKh { get; set; }

    public int? MaHh { get; set; }

    public int? SoLuong { get; set; }

    public double? ThanhTien { get; set; }

    public virtual HangHoa? MaHhNavigation { get; set; }

    public virtual KhachHang? MaKhNavigation { get; set; }
}
