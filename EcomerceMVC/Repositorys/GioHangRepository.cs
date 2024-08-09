
using EcomerceMVC.Data;
using EcomerceMVC.IRepositorys;
using EcomerceMVC.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EcomerceMVC.Repositorys
{
    public class GioHangRepository : IGioHangRepository
    {
        private readonly Hshop2023Context _context;

        public GioHangRepository(Hshop2023Context context) 
        {
            _context = context;
        }

		public async Task<int?> CreateHoaDon(HoaDon model , List<ChiTietGioHangViewModel> giohangs)
		{
            await _context.Database.BeginTransactionAsync();
            try
            {
               await _context.Database.CommitTransactionAsync();
               await _context.AddAsync(model);
               await _context.SaveChangesAsync();

                var listcthd = new List<ChiTietHd>();
                foreach(var item in giohangs)
                {
                    var cthd = new ChiTietHd
                    {
                        MaHd = model.MaHd,
                        SoLuong = item.SoLuong,
                        DonGia  = item.DonGia,
                        MaHh = item.MaHH,
                        GiamGia = 0,
                    };
                    listcthd.Add(cthd);
				}
                await _context.AddRangeAsync(listcthd);
                var result = await _context.SaveChangesAsync();
                return result;

			}
            catch
            {
               await _context.Database.RollbackTransactionAsync();
               return null;
            }
		}

		public async Task<List<ChiTietGioHangViewModel?>> GetGioHang(string makh)
		{
			var giohang = await _context.GioHangs.Include(x => x.MaKhNavigation).Include(x => x.MaHhNavigation).Where(x=>x.MaKh ==  makh).ToListAsync();
            if (giohang.Count <= 0)
            {
                return null;
            }    
            var result = giohang.Select(x => new ChiTietGioHangViewModel
            {
                MaHH = x.MaHh ?? 0 ,
                DonGia = x.MaHhNavigation.DonGia ?? 0,
                Hinh = x.MaHhNavigation?.Hinh,
                SoLuong = x.SoLuong ?? 0,
                TenHH = x.MaHhNavigation.TenHh,             
            }).ToList();
            return result;
		}

		public async Task<HangHoaViewModel> GetHangHoa(int id, int quantity)
        {
            var hanghoa = await _context.HangHoas.Where(x => x.MaHh == id).FirstOrDefaultAsync();
            if (hanghoa == null)
            {
                return null;
            }
            var result = new HangHoaViewModel
            {
                MaHH = hanghoa.MaHh,
                TenHH = hanghoa.TenHh,
                Hinh = hanghoa.Hinh ?? "",
                DonGia = hanghoa.DonGia ?? 0,
            };
            return result;
        }

		public async Task<int?> ThemHhVaoGioHang(int mahh, int soluong,string makh)
		{
            int? num = null;
			var hanghoatronggio = await _context.GioHangs.Include(x=>x.MaHhNavigation).Where(x => x.MaHh == mahh && x.MaKh == makh).FirstOrDefaultAsync();
            if(hanghoatronggio == null)
            {
                var hanghoa = await _context.HangHoas.FirstOrDefaultAsync(x=>x.MaHh == mahh);
                var result = new GioHang
                {
                    MaHh = mahh,
                    MaKh = makh,
                    SoLuong = soluong,
                    ThanhTien = soluong * hanghoa.DonGia
                };
                await _context.GioHangs.AddAsync(result);
				num = await _context.SaveChangesAsync();
				if (num == null)
				{
					return null;
				}
                return num;
			}
            else
            {
				hanghoatronggio.SoLuong += soluong;
				hanghoatronggio.ThanhTien = hanghoatronggio.SoLuong * hanghoatronggio.MaHhNavigation.DonGia;
                _context.GioHangs.Update(hanghoatronggio);
				num = await _context.SaveChangesAsync();
                if (num == null)
                {
                    return null;
                }
				return num;
			}    
		}

        public async Task<int?> GiamSlHhGioHang(int mahh, int soluong, string makh)
        {
            int? num = null;
            var hanghoatronggio = await _context.GioHangs.Include(x => x.MaHhNavigation).Where(x => x.MaHh == mahh && x.MaKh == makh).FirstOrDefaultAsync();
            if (hanghoatronggio == null)
            {
                return null;
            }
            if (hanghoatronggio.SoLuong > soluong) 
            {
                hanghoatronggio.SoLuong = hanghoatronggio.SoLuong - soluong;
                double thanhtien = (double)(soluong * hanghoatronggio.MaHhNavigation.DonGia);
                hanghoatronggio.ThanhTien = hanghoatronggio.ThanhTien - thanhtien;
                _context.Update(hanghoatronggio);
               num = await _context.SaveChangesAsync();
               if(num == null) { return null; }
               return num;
            }
             _context.Remove(hanghoatronggio);
            num = await _context.SaveChangesAsync();
            if (num == null) { return null;}
            return num;
        }

        public async Task<int?> XoaHhGioHang(int mahh, string makh)
        {
            int? num = null;
            var hanghoatronggio = await _context.GioHangs.Where(x => x.MaHh == mahh && x.MaKh == makh).FirstOrDefaultAsync();
            if (hanghoatronggio == null)
            {
                return null;
            }
            _context.Remove(hanghoatronggio);
            num = await _context.SaveChangesAsync();
            if (num == null) { return null;}
            return num;
        }
    }
}
