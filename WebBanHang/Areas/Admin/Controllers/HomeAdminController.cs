using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebBanHang.Areas.Admin.Models;
using WebBanHang.Data;

namespace WebBanHang.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeAdminController : Controller
    {
        private readonly Vshop2024Context db;

        public HomeAdminController(Vshop2024Context context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DangNhap(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            var model = new LoginModel();
            return View(model);
           
        }

        [HttpPost]
        public IActionResult DangNhap(LoginModel model, string returnUrl)
        {
            // Kiểm tra thông tin đăng nhập trong cơ sở dữ liệu
            ViewBag.returnUrl = returnUrl;
            var admin = db.NhanViens.SingleOrDefault(kh => kh.MaNv == model.UserName);
            if (admin == null)
            {
                ModelState.AddModelError("loi", "Không có nhân viên này!");
                return View(model);
            }
            if (admin.MatKhau != model.Password)
            {
                ModelState.AddModelError("loi", "Sai mật khẩu!");
                return View(model);
            }
            return View("Index");
        }

    }
}