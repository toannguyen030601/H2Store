using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace asm1.Entities;

public partial class AspmvcH2storeContext : DbContext
{
    public AspmvcH2storeContext()
    {
    }

    public AspmvcH2storeContext(DbContextOptions<AspmvcH2storeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DienThoai> DienThoais { get; set; }

    public virtual DbSet<DonDatHangChuaDktk> DonDatHangChuaDktks { get; set; }

    public virtual DbSet<LoaiDienThoai> LoaiDienThoais { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<QuanTriVien> QuanTriViens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=NGUYENDUCTOAN\\TOAN;Initial Catalog=aspmvcH2store;Persist Security Info=True;User ID=sa;Password=123;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DienThoai>(entity =>
        {
            entity.HasKey(e => e.MaDt).HasName("PK__DienThoa__27258655796E3031");

            entity.ToTable("DienThoai");

            entity.Property(e => e.MaDt).HasColumnName("MaDT");
            entity.Property(e => e.DungLuong)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gia).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.HinhAnh)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.MaLoaiDt).HasColumnName("MaLoaiDT");
            entity.Property(e => e.Mota).HasMaxLength(255);
            entity.Property(e => e.Ram)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TenDt)
                .HasMaxLength(255)
                .HasColumnName("TenDT");

            entity.HasOne(d => d.MaLoaiDtNavigation).WithMany(p => p.DienThoais)
                .HasForeignKey(d => d.MaLoaiDt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DienThoai_LoaiDienThoai");
        });

        modelBuilder.Entity<DonDatHangChuaDktk>(entity =>
        {
            entity.HasKey(e => e.MaDh).HasName("PK__DonDatHa__27258661535D669D");

            entity.ToTable("DonDatHangChuaDKTK");

            entity.Property(e => e.MaDh).HasColumnName("MaDH");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.HoTen).HasMaxLength(255);
            entity.Property(e => e.MaDt).HasColumnName("MaDT");
            entity.Property(e => e.NgayDat).HasColumnType("datetime");
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SDT");

            entity.HasOne(d => d.MaDtNavigation).WithMany(p => p.DonDatHangChuaDktks)
                .HasForeignKey(d => d.MaDt)
                .HasConstraintName("FK_DienThoai2");
        });

        modelBuilder.Entity<LoaiDienThoai>(entity =>
        {
            entity.HasKey(e => e.MaLoaiDt).HasName("PK__LoaiDien__122748674D4AA30D");

            entity.ToTable("LoaiDienThoai");

            entity.Property(e => e.MaLoaiDt).HasColumnName("MaLoaiDT");
            entity.Property(e => e.TenLoaiDt)
                .HasMaxLength(255)
                .HasColumnName("TenLoaiDT");
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaNd).HasName("PK__NguoiDun__2725D7243B2B0AF5");

            entity.ToTable("NguoiDung");

            entity.Property(e => e.MaNd).HasColumnName("MaND");
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.MatKhauNd)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("MatKhauND");
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.TaiKhoanNd)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TaiKhoanND");
            entity.Property(e => e.TenNd)
                .HasMaxLength(255)
                .HasColumnName("TenND");
        });

        modelBuilder.Entity<QuanTriVien>(entity =>
        {
            entity.HasKey(e => e.MaQtv).HasName("PK__QuanTriV__396E9996E3CAB0E6");

            entity.ToTable("QuanTriVien");

            entity.Property(e => e.MaQtv).HasColumnName("MaQTV");
            entity.Property(e => e.MatKhauQtv)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("MatKhauQTV");
            entity.Property(e => e.TaiKhoanQtv)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TaiKhoanQTV");
            entity.Property(e => e.TenQtv)
                .HasMaxLength(255)
                .HasColumnName("TenQTV");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
