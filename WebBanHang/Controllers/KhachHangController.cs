using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Security.Claims;
using WebBanHang.Data;
using WebBanHang.Helpers;
using WebBanHang.ViewModels;

namespace WebBanHang.Controllers
{
	public class KhachHangController : Controller
	{
		private readonly Vshop2024Context db;
		private readonly IMapper _mapper;

		public KhachHangController(Vshop2024Context context, IMapper mapper)
		{
			db = context;
			_mapper = mapper;
		}
		#region ĐăngKý
		[HttpGet]
		public IActionResult DangKy()
		{
			return View();
		}
		[HttpPost]
		public IActionResult DangKy(RegisterVM model, IFormFile Hinh)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var khachHang = _mapper.Map<KhachHang>(model);
					khachHang.RandomKey = MyUtil.GenerateRamdomKey();

					// Kiểm tra độ dài của chuỗi 'model.MaKh'
					int maxLength = 20; // Độ dài tối đa cho cột 'MaKh'
					if (model.MatKhau.Length > maxLength)
					{
						model.MatKhau = model.MaKh.Substring(0, maxLength); // Cắt chuỗi xuống độ dài tối đa
					}

					khachHang.MatKhau = model.MatKhau.ToMd5Hash(khachHang.RandomKey);
					khachHang.HieuLuc = true; // Sẽ xử lý khi dùng email active
					khachHang.VaiTro = 0;
					if (Hinh != null)
					{
						khachHang.Hinh = MyUtil.UpLoadHinh(Hinh, "KhachHang");
					}
					db.Add(khachHang);
					db.SaveChanges();
					return RedirectToAction("Index", "HangHoa");
				}
				catch (Exception ex)
				{
					// Xử lý lỗi
				}
			}
			return View();
		}
		#endregion

		#region Login
		[HttpGet]
		public IActionResult DangNhap(string? ReturnUrl)
		{
			ViewBag.ReturnUrl = ReturnUrl;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> DangNhap(LoginVM model, string? ReturnUrl)
		{
			ViewBag.ReturnUrl = ReturnUrl;
			if (ModelState.IsValid)
			{
				var khachHang = db.KhachHangs.SingleOrDefault(kh => kh.MaKh == model.UserName);
				if (khachHang == null)
				{
					ModelState.AddModelError("loi", "Không có khách hàng này");
				}
				else
				{
					if (!khachHang.HieuLuc)
					{
						ModelState.AddModelError("loi", "Tài khoản đã bị khóa. Vui lòng liên hệ Admin.");
					}
					else
					{
						if (khachHang.MatKhau != model.Password.ToMd5Hash(khachHang.RandomKey))
						{
							ModelState.AddModelError("loi", "Sai thông tin đăng nhập");
						}
						else
						{
							var claims = new List<Claim> {
								new Claim(ClaimTypes.Email, khachHang.Email),
								new Claim(ClaimTypes.Name, khachHang.HoTen),
								new Claim("CustomerID", khachHang.MaKh),

								//claim - role động
								new Claim(ClaimTypes.Role, "Customer")
							};

							var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
							var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

							await HttpContext.SignInAsync(claimsPrincipal);

							if (Url.IsLocalUrl(ReturnUrl))
							{
								return Redirect(ReturnUrl);
							}
							else
							{
								return Redirect("/");
							}
						}
					}
				}
			}
			return View();
		}
		#endregion

		[Authorize]
		public IActionResult Profile()
		{
			return View();
		}

		[Authorize]
		public async Task<IActionResult> DangXuat()
		{
			await HttpContext.SignOutAsync();
			return Redirect("/");
		}

	}
}
