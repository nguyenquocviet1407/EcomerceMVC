using AutoMapper;
using EcomerceMVC.Data;
using EcomerceMVC.Helpers;
using EcomerceMVC.IRepositorys;
using EcomerceMVC.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EcomerceMVC.Repositorys
{
	public class HangHoaRepository : IHangHoaRepository
	{
		private readonly Hshop2023Context _context;
		private readonly IMapper _mapper;

		public HangHoaRepository(Hshop2023Context context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<List<HangHoaViewModel>> GetHangHoa(int? maloai)
		{
			var hanghoas = await _context.HangHoas.Include(x => x.MaLoaiNavigation).ToListAsync();
			if (maloai.HasValue)
			{
				hanghoas = hanghoas.Where(x => x.MaLoai == maloai).ToList();
			}
			var resutl = hanghoas.Select(x => new HangHoaViewModel
			{
				MaHH = x.MaHh,
				TenHH = x.TenHh,
				Hinh = x.Hinh ?? "",
				DonGia = x.DonGia ?? 0,
				MoTaNgan = x.MoTaDonVi ?? "",
				MaLoai = maloai ?? 0,
				TenLoai = x.MaLoaiNavigation.TenLoai,
			}).ToList();

			return resutl;
		}

		public async Task<ChiTietHangHoaViewModel> GetDetailHangHoa(int id)
		{
			var DetailHh = await _context.HangHoas.Include(x => x.MaLoaiNavigation).Where(x => x.MaHh == id).FirstOrDefaultAsync();
			if (DetailHh == null)
			{
				return null;
			}
			var result = new ChiTietHangHoaViewModel
			{
				MaHH = DetailHh.MaHh,
				TenHH = DetailHh.TenHh,
				Hinh = DetailHh.Hinh ?? "",
				DonGia = DetailHh.DonGia ?? 0,
				MoTaNgan = DetailHh.MoTaDonVi ?? "",
				TenLoai = DetailHh.MaLoaiNavigation.TenLoai,
				ChiTiet = DetailHh.MoTa ?? string.Empty,
				SoLuongTon = 10,
				DiemDanhGia = 5,
			};
			return result;
		}

		public async Task<List<HangHoaViewModel>> SearchHangHoa(string? query)
		{
			var hanghoas = await _context.HangHoas.Include(x => x.MaLoaiNavigation).ToListAsync();
			if (!string.IsNullOrEmpty(query))
			{
				hanghoas = hanghoas.Where(x => x.TenHh.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
			}
			var result = hanghoas.Select(x => new HangHoaViewModel
			{
				MaHH = x.MaHh,
				TenHH = x.TenHh,
				Hinh = x.Hinh ?? "",
				DonGia = x.DonGia ?? 0,
				MoTaNgan = x.MoTaDonVi ?? "",
				TenLoai = x.MaLoaiNavigation.TenLoai,
			}).ToList();

			return result;
		}

		public async Task<List<HangHoaModel?>> GetHangHoaAll()
		{
			var hanghoas = await _context.HangHoas.Include(x => x.MaNccNavigation).Include(x => x.MaLoaiNavigation).OrderByDescending(x => x.MaHh).ToListAsync();
			if (hanghoas == null)
			{
				return null;
			}
			var result = hanghoas.Select(x => new HangHoaModel
			{
				MaHh = x.MaHh,
				TenHh = x.TenHh,
				TenAlias = x.TenAlias,
				MaLoai = x.MaLoai,
				TenLoai = x.MaLoaiNavigation.TenLoai,
				MoTaDonVi = x.MoTaDonVi,
				DonGia = x.DonGia,
				Hinh = x.Hinh,
				NgaySx = x.NgaySx,
				GiamGia = x.GiamGia,
				SoLanXem = x.SoLanXem,
				MoTa = x.MoTa,
				NhaCC = x.MaNccNavigation.TenCongTy,
			}).ToList();
			return result;

		}

		public async Task<List<LoaiHangHoaViewModel>> GetLoaiHangHoa()
		{
			var result = await _context.Loais.ToListAsync();
			return _mapper.Map<List<LoaiHangHoaViewModel>>(result);
		}

		public async Task<List<NhaCungCapViewModel>> GetNhaCungCap()
		{
			var result = await _context.NhaCungCaps.ToListAsync();
			return _mapper.Map<List<NhaCungCapViewModel>>(result);
		}

		public async Task<HangHoaModel?> CreateHangHoa(NhapHangHoaViewModel model)
		{
			var hanghoa = _mapper.Map<HangHoa>(model);
			hanghoa.GiamGia = 0;
			hanghoa.SoLanXem = 0;
			if (model.FileHinh != null)
			{
				hanghoa.Hinh = MyUtil.UpLoadHinh(model.FileHinh, "HangHoa");
			}

			await _context.AddAsync(hanghoa);
			int? num = await _context.SaveChangesAsync();

			if (num == null)
			{
				return null;
			}
			var loaihanghoa = await _context.Loais.FirstOrDefaultAsync(x =>x.MaLoai == hanghoa.MaLoai);
			var nhacc = await _context.NhaCungCaps.FirstOrDefaultAsync(x =>x.MaNcc == hanghoa.MaNcc);
			var result =  new HangHoaModel
			{
				MaHh = hanghoa.MaHh,
				TenHh = hanghoa.TenHh,
				TenAlias = hanghoa.TenAlias,
				TenLoai = loaihanghoa.TenLoai,
				MoTaDonVi = hanghoa.MoTaDonVi,
				DonGia = hanghoa.DonGia,
				Hinh = hanghoa.Hinh,
				NgaySx = hanghoa.NgaySx,
				GiamGia = hanghoa.GiamGia,
				SoLanXem = hanghoa.SoLanXem,
				MoTa = hanghoa.MoTa,
				NhaCC = nhacc.TenCongTy,
			};
			return result ;
		}

		public async Task<int?> DeleteHangHoa(int mahh)
		{
			var hanghoa = await _context.HangHoas.FindAsync(mahh);
			if (hanghoa == null)
			{
				return null;
			}

			if (hanghoa.Hinh != null) 
			{
				MyUtil.DeleteHinh(hanghoa.Hinh,"HangHoa");
			}	
			_context.Remove(hanghoa);
			int? result =  await _context.SaveChangesAsync();
			return result;
		}

		public async Task<CapNhatHangHoaModel> GetHangHoaById(int mahh)
		{
			var hanghoa = await _context.HangHoas.FirstOrDefaultAsync(x => x.MaHh == mahh);
			return _mapper.Map<CapNhatHangHoaModel>(hanghoa);
		}

		public async Task<int?> UpdateHangHoa(CapNhatHangHoaModel model)
		{
			var hanghoa = await _context.HangHoas.FindAsync(model.MaHh);
			if (hanghoa == null)
			{
				return null;
			}
			if (model.FileHinh != null)
			{
				hanghoa.Hinh = MyUtil.UpLoadHinh(model.FileHinh, "HangHoa");
			}
			hanghoa.TenHh = model.TenHh;
			hanghoa.TenAlias = model.TenAlias;
			hanghoa.MaLoai = model.MaLoai;
			hanghoa.MoTaDonVi = model.MoTaDonVi;
			hanghoa.DonGia = model.DonGia;
			hanghoa.NgaySx = model.NgaySx;
			hanghoa.MoTa = model.MoTa;
			hanghoa.MaNcc = model.MaNcc;

			_context.Update(hanghoa);
			int? result =  await _context.SaveChangesAsync();

			return result;
		}
	}
}
