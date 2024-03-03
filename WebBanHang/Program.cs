using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebBanHang.Data;
using WebBanHang.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Vshop2024Context>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("VShop2024")); });

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddMvc().AddRazorOptions(options =>
{
    options.AreaPageViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}.cshtml");
}).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/KhachHang/DangNhap";
		options.AccessDeniedPath = "/AccessDenied";
	});

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
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
       name: "default",
       pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
     name: "Admin",
     pattern: "/admin/{controller=HomeAdmin}/{action=DangNhap}/{id?}",
     defaults: new { area = "Admin" }
 );
   
});
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//        name: "Admin",
//        pattern: "/admin/{controller=HomeAdmin}/{action=Index}/{id?}",
//        defaults: new { area = "Admin" }
//    );

//    endpoints.MapControllerRoute(
//        name: "default",
//        pattern: "{controller=Home}/{action=Index}/{id?}"
//    );
//});
app.Run();
