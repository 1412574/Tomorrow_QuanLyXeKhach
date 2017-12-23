namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initall : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ChiTietDatVes", "maDatVe", "dbo.DatVes");
            DropForeignKey("dbo.DatVes", "maChuyenXe", "dbo.ChuyenXes");
            DropForeignKey("dbo.DatVes", "maKhachHang", "dbo.KhachHangs");
            DropIndex("dbo.ChiTietDatVes", new[] { "maDatVe" });
            DropIndex("dbo.DatVes", new[] { "maChuyenXe" });
            DropIndex("dbo.DatVes", new[] { "maKhachHang" });
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
                    })
                .PrimaryKey(t => t.maNV)
                .ForeignKey("dbo.TrangThaiNV", t => t.maTT, cascadeDelete: true)
                .ForeignKey("dbo.VaiTro", t => t.maVT, cascadeDelete: true)
                .Index(t => t.maVT)
                .Index(t => t.maTT);
            
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
            
            DropColumn("dbo.TuyenXes", "GiaVe");
            DropTable("dbo.ChiTietDatVes");
            DropTable("dbo.DatVes");
            DropTable("dbo.KhachHangs");
        }
        
        public override void Down()
        {
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
                "dbo.DatVes",
                c => new
                    {
                        maDatVe = c.Int(nullable: false, identity: true),
                        maChuyenXe = c.Int(),
                        maKhachHang = c.Int(),
                        tongTien = c.Double(),
                        ngayDat = c.DateTime(),
                        trangThai = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.maDatVe);
            
            CreateTable(
                "dbo.ChiTietDatVes",
                c => new
                    {
                        maCTDV = c.Int(nullable: false, identity: true),
                        maDatVe = c.Int(nullable: false),
                        giaTien = c.Double(),
                        soGhe = c.Int(),
                    })
                .PrimaryKey(t => t.maCTDV);
            
            AddColumn("dbo.TuyenXes", "GiaVe", c => c.Int(nullable: false));
            DropForeignKey("dbo.NhanVien", "maVT", "dbo.VaiTro");
            DropForeignKey("dbo.NhanVien", "maTT", "dbo.TrangThaiNV");
            DropForeignKey("dbo.TaiKhoanNV", "maNV", "dbo.NhanVien");
            DropIndex("dbo.TaiKhoanNV", new[] { "maNV" });
            DropIndex("dbo.NhanVien", new[] { "maTT" });
            DropIndex("dbo.NhanVien", new[] { "maVT" });
            DropTable("dbo.VaiTro");
            DropTable("dbo.TrangThaiNV");
            DropTable("dbo.TaiKhoanNV");
            DropTable("dbo.NhanVien");
            CreateIndex("dbo.DatVes", "maKhachHang");
            CreateIndex("dbo.DatVes", "maChuyenXe");
            CreateIndex("dbo.ChiTietDatVes", "maDatVe");
            AddForeignKey("dbo.DatVes", "maKhachHang", "dbo.KhachHangs", "maKhachHang");
            AddForeignKey("dbo.DatVes", "maChuyenXe", "dbo.ChuyenXes", "MaChuyenXe");
            AddForeignKey("dbo.ChiTietDatVes", "maDatVe", "dbo.DatVes", "maDatVe", cascadeDelete: true);
        }
    }
}
