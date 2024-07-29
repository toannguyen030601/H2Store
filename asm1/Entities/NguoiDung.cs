using System;
using System.Collections.Generic;

namespace asm1.Entities;

public partial class NguoiDung
{
    public int MaNd { get; set; }

    public string TenNd { get; set; } = null!;

    public string TaiKhoanNd { get; set; } = null!;

    public string MatKhauNd { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Sdt { get; set; } = null!;

    public string DiaChi { get; set; } = null!;
}
