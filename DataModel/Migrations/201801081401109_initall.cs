namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initall : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BangChamCongs",
                c => new
                    {
                        maCC = c.Int(nullable: false, identity: true),
                        maNV = c.Int(nullable: false),
                        ngay = c.DateTime(nullable: false),
                        gioBatDau = c.DateTime(nullable: false),
                        gioKetThuc = c.DateTime(nullable: false),
                        ghiChu = c.String(),
                    })
                .PrimaryKey(t => t.maCC)
                .ForeignKey("dbo.NhanVien", t => t.maNV, cascadeDelete: true)
                .Index(t => t.maNV);
            
            CreateTable(
                "dbo.NhanVien",
                c => new
                    {
                        maNV = c.Int(nullable: false, identity: true),
                        hoTen = c.String(maxLength: 30),
                        cCCD = c.String(maxLength: 12),
                        bangCap = c.String(maxLength: 30),
                        sDT = c.String(maxLength: 12),
                        diaChi = c.String(maxLength: 100),
                        luongCanBan = c.Single(nullable: false),
                        maVT = c.Int(nullable: false),
                        maTT = c.Int(nullable: false),
                        maPB = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.maNV)
                .ForeignKey("dbo.PhongBan", t => t.maPB, cascadeDelete: true)
                .ForeignKey("dbo.TrangThaiNV", t => t.maTT, cascadeDelete: true)
                .ForeignKey("dbo.VaiTro", t => t.maVT, cascadeDelete: true)
                .Index(t => t.maVT)
                .Index(t => t.maTT)
                .Index(t => t.maPB);
            
            CreateTable(
                "dbo.PhanCongs",
                c => new
                    {
                        maPC = c.Int(nullable: false, identity: true),
                        maNV = c.Int(nullable: false),
                        maCV = c.Int(nullable: false),
                        ngayPC = c.DateTime(nullable: false),
                        danhGia = c.String(),
                        nhiemVu = c.String(),
                        ghiChu = c.String(),
                    })
                .PrimaryKey(t => t.maPC)
                .ForeignKey("dbo.CongViecs", t => t.maCV, cascadeDelete: true)
                .ForeignKey("dbo.NhanVien", t => t.maNV, cascadeDelete: true)
                .Index(t => t.maNV)
                .Index(t => t.maCV);
            
            CreateTable(
                "dbo.CongViecs",
                c => new
                    {
                        maCV = c.Int(nullable: false, identity: true),
                        tenCV = c.String(maxLength: 100),
                        yeuCauCV = c.String(),
                        moTaCV = c.String(),
                        hanHoanThanh = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.maCV);
            
            CreateTable(
                "dbo.PhongBan",
                c => new
                    {
                        maPB = c.Int(nullable: false, identity: true),
                        tenPB = c.String(maxLength: 50),
                        moTaPB = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.maPB);
            
            CreateTable(
                "dbo.TaiKhoanNV",
                c => new
                    {
                        maTK = c.Int(nullable: false, identity: true),
                        matKhau = c.String(maxLength: 40),
                        maNV = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.maTK)
                .ForeignKey("dbo.NhanVien", t => t.maNV, cascadeDelete: true)
                .Index(t => t.maNV);
            
            CreateTable(
                "dbo.TrangThaiNV",
                c => new
                    {
                        maTT = c.Int(nullable: false, identity: true),
                        tenTT = c.String(maxLength: 30),
                        moTaTT = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.maTT);
            
            CreateTable(
                "dbo.VaiTro",
                c => new
                    {
                        maVT = c.Int(nullable: false, identity: true),
                        tenVT = c.String(maxLength: 50),
                        moTaVT = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.maVT);
            
            CreateTable(
                "dbo.BaoCao",
                c => new
                    {
                        maBienBan = c.Int(nullable: false, identity: true),
                        tenBienBan = c.String(maxLength: 50),
                        noiDung = c.String(maxLength: 200),
                        ngayLapBaoCao = c.DateTime(nullable: false),
                        ngaySuaDoi = c.DateTime(nullable: false),
                        lanSuaDoi = c.Int(nullable: false),
                        ghiChu = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.maBienBan);
            
            CreateTable(
                "dbo.ChiTietDatVes",
                c => new
                    {
                        maCTDV = c.Int(nullable: false, identity: true),
                        giaTien = c.Double(),
                        soGhe = c.Int(nullable: false),
                        maDatVe = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.maCTDV)
                .ForeignKey("dbo.DatVes", t => t.maDatVe, cascadeDelete: true)
                .Index(t => t.maDatVe);
            
            CreateTable(
                "dbo.DatVes",
                c => new
                    {
                        maDatVe = c.Int(nullable: false, identity: true),
                        maKhachHang = c.Int(),
                        tongTien = c.Double(),
                        ngayDat = c.DateTime(),
                        trangThai = c.Boolean(nullable: false),
                        maChuyenXe = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.maDatVe)
                .ForeignKey("dbo.ChuyenXes", t => t.maChuyenXe, cascadeDelete: true)
                .ForeignKey("dbo.KhachHangs", t => t.maKhachHang)
                .Index(t => t.maKhachHang)
                .Index(t => t.maChuyenXe);
            
            CreateTable(
                "dbo.ChuyenXes",
                c => new
                    {
                        MaChuyenXe = c.Int(nullable: false, identity: true),
                        TenChuyenXe = c.String(),
                        NgayGioChay = c.DateTime(nullable: false),
                        TaiXe = c.Int(nullable: false),
                        MaTuyenXe = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaChuyenXe)
                .ForeignKey("dbo.TuyenXe", t => t.MaTuyenXe, cascadeDelete: true)
                .Index(t => t.MaTuyenXe);
            
            CreateTable(
                "dbo.TuyenXe",
                c => new
                    {
                        MaTuyenXe = c.Int(nullable: false, identity: true),
                        TenTuyenXe = c.String(maxLength: 50),
                        GiaVe = c.Int(nullable: false),
                        LoaiXe = c.String(maxLength: 50),
                        ThoiGian = c.Int(nullable: false),
                        QuangDuong = c.Int(nullable: false),
                        SoChuyen = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaTuyenXe);
            
            CreateTable(
                "dbo.HanhTrinhs",
                c => new
                    {
                        MaHanhTrinh = c.Int(nullable: false, identity: true),
                        MaTuyenXe = c.Int(nullable: false),
                        MaTramXe = c.Int(nullable: false),
                        ThuTu = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaHanhTrinh)
                .ForeignKey("dbo.TramXes", t => t.MaTramXe, cascadeDelete: true)
                .ForeignKey("dbo.TuyenXe", t => t.MaTuyenXe, cascadeDelete: true)
                .Index(t => t.MaTuyenXe)
                .Index(t => t.MaTramXe);
            
            CreateTable(
                "dbo.TramXes",
                c => new
                    {
                        MaTramXe = c.Int(nullable: false, identity: true),
                        TenTramXe = c.String(maxLength: 50),
                        DiaChi = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.MaTramXe);
            
            CreateTable(
                "dbo.KhachHangs",
                c => new
                    {
                        maKhachHang = c.Int(nullable: false, identity: true),
                        tenKhachHang = c.String(maxLength: 100),
                        soDienThoai = c.String(maxLength: 15),
                        taiKhoan = c.String(maxLength: 60),
                    })
                .PrimaryKey(t => t.maKhachHang);
            
            CreateTable(
                "dbo.LichPhongVan",
                c => new
                    {
                        maLPV = c.Int(nullable: false, identity: true),
                        ngay = c.DateTime(nullable: false),
                        diaDiem = c.String(),
                        tieuChi = c.String(),
                        ghiChu = c.String(),
                    })
                .PrimaryKey(t => t.maLPV);
            
            CreateTable(
                "dbo.UngVien",
                c => new
                    {
                        maUV = c.Int(nullable: false, identity: true),
                        hoTen = c.String(maxLength: 30),
                        sDT = c.String(maxLength: 15),
                        email = c.String(maxLength: 75),
                        trangThai = c.Int(nullable: false),
                        maLPV = c.Int(),
                        TrangThaiUV_maTT = c.Int(),
                    })
                .PrimaryKey(t => t.maUV)
                .ForeignKey("dbo.LichPhongVan", t => t.maLPV)
                .ForeignKey("dbo.TrangThaiUV", t => t.TrangThaiUV_maTT)
                .Index(t => t.maLPV)
                .Index(t => t.TrangThaiUV_maTT);
            
            CreateTable(
                "dbo.TrangThaiUV",
                c => new
                    {
                        maTT = c.Int(nullable: false, identity: true),
                        tenTT = c.String(),
                        moTaTT = c.String(),
                    })
                .PrimaryKey(t => t.maTT);
            
            CreateTable(
                "dbo.ThongKe",
                c => new
                    {
                        maBienBan = c.Int(nullable: false, identity: true),
                        tenBienBan = c.String(maxLength: 50),
                        noiDung = c.String(maxLength: 200),
                        ngayLapThongKe = c.DateTime(nullable: false),
                        ngaySuaDoi = c.DateTime(nullable: false),
                        lanSuaDoi = c.Int(nullable: false),
                        ghiChu = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.maBienBan);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UngVien", "TrangThaiUV_maTT", "dbo.TrangThaiUV");
            DropForeignKey("dbo.UngVien", "maLPV", "dbo.LichPhongVan");
            DropForeignKey("dbo.DatVes", "maKhachHang", "dbo.KhachHangs");
            DropForeignKey("dbo.HanhTrinhs", "MaTuyenXe", "dbo.TuyenXe");
            DropForeignKey("dbo.HanhTrinhs", "MaTramXe", "dbo.TramXes");
            DropForeignKey("dbo.ChuyenXes", "MaTuyenXe", "dbo.TuyenXe");
            DropForeignKey("dbo.DatVes", "maChuyenXe", "dbo.ChuyenXes");
            DropForeignKey("dbo.ChiTietDatVes", "maDatVe", "dbo.DatVes");
            DropForeignKey("dbo.BangChamCongs", "maNV", "dbo.NhanVien");
            DropForeignKey("dbo.NhanVien", "maVT", "dbo.VaiTro");
            DropForeignKey("dbo.NhanVien", "maTT", "dbo.TrangThaiNV");
            DropForeignKey("dbo.TaiKhoanNV", "maNV", "dbo.NhanVien");
            DropForeignKey("dbo.NhanVien", "maPB", "dbo.PhongBan");
            DropForeignKey("dbo.PhanCongs", "maNV", "dbo.NhanVien");
            DropForeignKey("dbo.PhanCongs", "maCV", "dbo.CongViecs");
            DropIndex("dbo.UngVien", new[] { "TrangThaiUV_maTT" });
            DropIndex("dbo.UngVien", new[] { "maLPV" });
            DropIndex("dbo.HanhTrinhs", new[] { "MaTramXe" });
            DropIndex("dbo.HanhTrinhs", new[] { "MaTuyenXe" });
            DropIndex("dbo.ChuyenXes", new[] { "MaTuyenXe" });
            DropIndex("dbo.DatVes", new[] { "maChuyenXe" });
            DropIndex("dbo.DatVes", new[] { "maKhachHang" });
            DropIndex("dbo.ChiTietDatVes", new[] { "maDatVe" });
            DropIndex("dbo.TaiKhoanNV", new[] { "maNV" });
            DropIndex("dbo.PhanCongs", new[] { "maCV" });
            DropIndex("dbo.PhanCongs", new[] { "maNV" });
            DropIndex("dbo.NhanVien", new[] { "maPB" });
            DropIndex("dbo.NhanVien", new[] { "maTT" });
            DropIndex("dbo.NhanVien", new[] { "maVT" });
            DropIndex("dbo.BangChamCongs", new[] { "maNV" });
            DropTable("dbo.ThongKe");
            DropTable("dbo.TrangThaiUV");
            DropTable("dbo.UngVien");
            DropTable("dbo.LichPhongVan");
            DropTable("dbo.KhachHangs");
            DropTable("dbo.TramXes");
            DropTable("dbo.HanhTrinhs");
            DropTable("dbo.TuyenXe");
            DropTable("dbo.ChuyenXes");
            DropTable("dbo.DatVes");
            DropTable("dbo.ChiTietDatVes");
            DropTable("dbo.BaoCao");
            DropTable("dbo.VaiTro");
            DropTable("dbo.TrangThaiNV");
            DropTable("dbo.TaiKhoanNV");
            DropTable("dbo.PhongBan");
            DropTable("dbo.CongViecs");
            DropTable("dbo.PhanCongs");
            DropTable("dbo.NhanVien");
            DropTable("dbo.BangChamCongs");
        }
    }
}
