using Microsoft.AspNetCore.Mvc;
using WebBanHang.Data;
using WebBanHang.ViewModels;

namespace WebBanHang.ViewComponents
{
    public class MenuLoaiViewComponent : ViewComponent
    {
        private readonly Vshop2024Context db;

        public MenuLoaiViewComponent(Vshop2024Context context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Loais.Select(loai => new MenuLoaiVM
            {
                MaLoai = loai.MaLoai,
                TenLoai = loai.TenLoai,
                SoLuong = loai.HangHoas.Count
            }).OrderBy(p => p.TenLoai);
            return View(data); //Default.cshtml
        }



    }
}
