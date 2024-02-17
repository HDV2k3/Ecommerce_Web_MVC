namespace WebBanHang.ViewModels
{
    public class HangHoaVM
    {
        public int MaHh { get; set; }
        public required String TenHh { get; set; }
        public required String Hinh { get; set; }   
        public double DonGia { get; set; }   
        public required String MoTaNgan { get; set; }   
        public required String TenLoai { get; set; }   
    }
    public class CTHangHoaVM
    {
        public int MaHh { get; set; }
        public required String TenHh { get; set; }
        public required String Hinh { get; set; }
        public double DonGia { get; set; }
        public required String MoTaNgan { get; set; }
        public required String TenLoai { get; set; }
        public required String ChiTiet { get; set; }
        public required int DiemDanhGia { get; set; }
        public required int SoLuongTon { get; set; }
    }
}
