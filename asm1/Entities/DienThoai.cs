using System;
using System.Collections.Generic;

namespace asm1.Entities;

public partial class DienThoai
{
    public int MaDt { get; set; }

    public string TenDt { get; set; } = null!;

    public decimal Gia { get; set; }

    public string? Ram { get; set; }

    public string? DungLuong { get; set; }

    public int MaLoaiDt { get; set; }

    public string? HinhAnh { get; set; }

    public string? Mota { get; set; }

    public virtual ICollection<DonDatHangChuaDktk> DonDatHangChuaDktks { get; set; } = new List<DonDatHangChuaDktk>();

    public virtual LoaiDienThoai MaLoaiDtNavigation { get; set; } = null!;
}
