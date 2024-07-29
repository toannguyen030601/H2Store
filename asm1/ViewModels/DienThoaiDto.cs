using Microsoft.Build.Framework;

namespace asm1.ViewModels
{
    public class DienThoaiDto
    {
        [Required]
        public string TenDt { get; set; } = null!;

        [Required]
        public decimal Gia { get; set; }
        [Required]
        public string? Ram { get; set; }
        [Required]
        public string? DungLuong { get; set; }
        [Required]
        public int MaLoaiDt { get; set; }

        public IFormFile? FileHinhAnh { get; set; }
        [Required]
        public string? Mota { get; set; }
    }
}
