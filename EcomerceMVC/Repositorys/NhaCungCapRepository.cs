//using AspNetCore;
using AutoMapper;
using EcomerceMVC.Data;
using EcomerceMVC.Helpers;
using EcomerceMVC.IRepositorys;
using EcomerceMVC.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EcomerceMVC.Repositorys
{
	public class NhaCungCapRepository : INhaCungCapRepository
	{
		private readonly Hshop2023Context _context;
		private readonly IMapper _mapper;

		public NhaCungCapRepository(Hshop2023Context context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<int?> CapNhatNhaCungCap(NhapNhaCungCapViewModel model)
		{
			var nhacc = await _context.NhaCungCaps.FirstOrDefaultAsync(x => x.MaNcc == model.MaNcc);
			if (nhacc == null)
			{
				return null;
			}
			if (model.FileHinh != null)
			{
				nhacc.Logo = MyUtil.UpLoadHinh(model.FileHinh, "NhaCC");
			}
			nhacc.TenCongTy = model.TenCongTy;	
			nhacc.NguoiLienLac = model.NguoiLienLac;
			nhacc.DiaChi = model.DiaChi;
			nhacc.DienThoai = model.DienThoai;
			nhacc.Email = model.Email;
			nhacc.MoTa = model.MoTa;

			_context.Update(nhacc);
			int? result =  await _context.SaveChangesAsync();
			if (result == null)
			{
				return null;
			}

			return result;
		}

		public async Task<List<NhapNhaCungCapViewModel>> GetAllNhaCungCap()
		{
			var nhacungcaps = await _context.NhaCungCaps.ToListAsync();
			return _mapper.Map<List<NhapNhaCungCapViewModel>>(nhacungcaps);
		}

		public async Task<NhapNhaCungCapViewModel?> GetNhaCungCapById(string mancc)
		{
			var nhacc = await _context.NhaCungCaps.FirstOrDefaultAsync(x => x.MaNcc == mancc);
			if (nhacc == null)
			{
				return null;
			}
			return _mapper.Map<NhapNhaCungCapViewModel>(nhacc);
		}

		public async Task<NhapNhaCungCapViewModel?> NhapNhaCungCap(NhapNhaCungCapViewModel model)
		{
			try
			{
				//var nhacc =  await _context.NhaCungCaps.FirstOrDefaultAsync(x => x.MaNcc == model.MaNcc);
				//if (nhacc != null)
				//{
				//	return null;
				//}
				if (model.FileHinh != null)
				{
					model.Logo = MyUtil.UpLoadHinh(model.FileHinh, "NhaCC");
				}
				model.Logo = "";
				var nhacc = _mapper.Map<NhaCungCap>(model);
				await _context.AddAsync(nhacc);
				int? num = await _context.SaveChangesAsync();

				if (num == null)
				{
					return null;
				}
				return model;
			}
			catch(Exception ex)
			{
				return null;
			}
		}

		public async Task<int?> XoaNhaCungCap(string mancc)
		{
			var nhacc = await _context.NhaCungCaps.FirstOrDefaultAsync(x => x.MaNcc == mancc);
			if (nhacc == null)
			{
				return null;
			}
			if (nhacc.Logo != null)
			{
				MyUtil.DeleteHinh(nhacc.Logo, "NhaCC");
			}
			_context.Remove(nhacc);
			int? result = await _context.SaveChangesAsync();
			if(result == null)
			{
				return null;
			}	
			return result;
		}
	}
}
