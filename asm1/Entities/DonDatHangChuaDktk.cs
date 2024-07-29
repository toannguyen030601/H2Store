using System;
using System.Collections.Generic;

namespace asm1.Entities;

public partial class DonDatHangChuaDktk
{
    public int MaDh { get; set; }

    public string? HoTen { get; set; }

    public string? Email { get; set; }

    public string? Sdt { get; set; }

    public string? DiaChi { get; set; }

    public int? MaDt { get; set; }

    public int? SoLuong { get; set; }

    public DateTime? NgayDat { get; set; }

    public bool? TrangThai { get; set; }

    public virtual DienThoai? MaDtNavigation { get; set; }
}
