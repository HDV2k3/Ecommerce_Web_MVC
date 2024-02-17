using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using WebBanHang.Data;
using WebBanHang.Helpers;
using WebBanHang.ViewModels;

namespace WebBanHang.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly Vshop2024Context db;
        private readonly IMapper _mapper;

        public KhachHangController(Vshop2024Context context,IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }
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
                    if (model.MaKh.Length > maxLength)
                    {
                        model.MaKh = model.MaKh.Substring(0, maxLength); // Cắt chuỗi xuống độ dài tối đa
                    }

                    khachHang.MaKh = model.MaKh.ToMd5Hash(khachHang.RandomKey);
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

    }
}
