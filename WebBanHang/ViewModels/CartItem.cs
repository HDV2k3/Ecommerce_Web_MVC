namespace WebBanHang.ViewModels
{
    public class CartItem
    {
        public int MaHH { get; set; }
        public required string Hinh { get; set; }
        public required string TenHH { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien => SoLuong * DonGia;
    }
}
