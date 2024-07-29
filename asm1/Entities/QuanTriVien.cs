using System;
using System.Collections.Generic;

namespace asm1.Entities;

public partial class QuanTriVien
{
    public int MaQtv { get; set; }

    public string TaiKhoanQtv { get; set; } = null!;

    public string MatKhauQtv { get; set; } = null!;

    public string TenQtv { get; set; } = null!;
}
