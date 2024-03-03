using System;
using System.Collections.Generic;

namespace WebBanHang.Data;

public partial class YeuThich
{
    public int MaYt { get; set; }

    public int? MaHh { get; set; }

    public string MaKh { get; set; } = null!;

    public DateTime? NgayChon { get; set; }

    public string? MoTa { get; set; }

    public virtual HangHoa? MaHhNavigation { get; set; }

    public virtual KhachHang MaKhNavigation { get; set; } = null!;
}
