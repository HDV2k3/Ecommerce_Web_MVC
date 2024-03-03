using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanHang.Data;
using WebBanHang.ViewModels;

namespace WebBanHang.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly Vshop2024Context db;

        public HangHoaController(Vshop2024Context context)
        {
            db = context;
        }
        public IActionResult Index(int? loai)
        {
            var hanghoas = db.HangHoas.AsQueryable();
            if (loai.HasValue)
            {
                hanghoas = hanghoas.Where(p => p.MaLoai == loai.Value);

            }
            var result = hanghoas.Select(p => new HangHoaVM
            {
                MaHh = p.MaHh,
                TenHh = p.TenHh,
                DonGia = p.DonGia ?? 0,
                Hinh = p.Hinh ?? "",
                MoTaNgan = p.MoTaDonVi ?? "",
                TenLoai = p.MaLoaiNavigation.TenLoai
            });
            return View(result);
        }
        public IActionResult Search(String? query)
        {
            var hanghoas = db.HangHoas.AsQueryable();
            if (query != null)
            {
                hanghoas = hanghoas.Where(p => p.TenHh.Contains(query));

            }
            var result = hanghoas.Select(p => new HangHoaVM
            {
                MaHh = p.MaHh,
                TenHh = p.TenHh,
                DonGia = p.DonGia ?? 0,
                Hinh = p.Hinh ?? "",
                MoTaNgan = p.MoTaDonVi ?? "",
                TenLoai = p.MaLoaiNavigation.TenLoai
            });
            return View(result);
        }
        public IActionResult Detail(int id)
        {
            var data = db.HangHoas.Include(p => p.MaLoaiNavigation)
                .SingleOrDefault(p => p.MaHh == id);
            if (data == null)
            {
                TempData["Message"] = $"Khong Tim Thay San Pham Co Ma {id}";
                return Redirect("/404");
            }
            var result = new CTHangHoaVM
            {
                MaHh = data.MaHh,
                TenHh = data.TenHh,
                DonGia = data.DonGia ?? 0,
                ChiTiet = data.MoTa ?? String.Empty,
                Hinh = data.Hinh ?? String.Empty,
                MoTaNgan = data.MoTaDonVi ?? String.Empty,
                TenLoai = data.MaLoaiNavigation.TenLoai,
                SoLuongTon = 10,
                DiemDanhGia = 5,

            };
            return View(result);
        }
    }

}
