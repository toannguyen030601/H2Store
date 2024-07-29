using System;
using System.Collections.Generic;

namespace asm1.Entities;

public partial class LoaiDienThoai
{
    public int MaLoaiDt { get; set; }

    public string TenLoaiDt { get; set; } = null!;

    public virtual ICollection<DienThoai> DienThoais { get; set; } = new List<DienThoai>();
}
