using EcomerceMVC.Data;
using EcomerceMVC.Helpers;
using EcomerceMVC.IRepositorys;
using EcomerceMVC.Repositorys;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// đăng ký dbcontext liên kết database
builder.Services.AddDbContext<Hshop2023Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("HShop"));
});


builder.Services.AddDistributedMemoryCache();
// Thêm dịch vụ Session 
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// đăng ký Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
        options.LoginPath = "/KhachHang/DangNhap";
        options.AccessDeniedPath = "/AccessDenied";
		options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
        options.SlidingExpiration = true;

	});

// đăng ký AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// đăng ký Repository
builder.Services.AddScoped<IHangHoaRepository,HangHoaRepository>();
builder.Services.AddScoped<IGioHangRepository,GioHangRepository>();
builder.Services.AddScoped<IKhachHangRepository,KhachHangRepository>();
builder.Services.AddScoped<INhaCungCapRepository,NhaCungCapRepository>();
builder.Services.AddScoped<IHoaDonRepository, HoaDonRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapAreaControllerRoute(
	 name: "admin",
	 areaName: "admin",
	 pattern: "admin/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapAreaControllerRoute(
//    name: "Admin",
//    areaName: "Admin",
//    pattern: "Admin/{controller=DanhMucHangHoa}/{action=Index}/{id?}");


app.Run();
