namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateinit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChuyenXes",
                c => new
                    {
                        MaChuyenXe = c.Int(nullable: false, identity: true),
                        MaTuyenXe = c.Int(nullable: false),
                        TenChuyenXe = c.String(),
                        NgayGioChay = c.DateTime(nullable: false),
                        TaiXe = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaChuyenXe)
                .ForeignKey("dbo.TuyenXes", t => t.MaTuyenXe, cascadeDelete: true)
                .Index(t => t.MaTuyenXe);
            
            CreateTable(
                "dbo.TuyenXes",
                c => new
                    {
                        MaTuyenXe = c.Int(nullable: false, identity: true),
                        TenTuyenXe = c.String(),
                    })
                .PrimaryKey(t => t.MaTuyenXe);
            
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
            
            CreateTable(
                "dbo.PhongBan",
                c => new
                    {
                        maPB = c.Int(nullable: false, identity: true),
                        tenPB = c.String(maxLength: 50),
                        moTaPB = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.maPB);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NhanVien", "maVT", "dbo.VaiTro");
            DropForeignKey("dbo.NhanVien", "maTT", "dbo.TrangThaiNV");
            DropForeignKey("dbo.TaiKhoanNV", "maNV", "dbo.NhanVien");
            DropForeignKey("dbo.ChuyenXes", "MaTuyenXe", "dbo.TuyenXes");
            DropIndex("dbo.TaiKhoanNV", new[] { "maNV" });
            DropIndex("dbo.NhanVien", new[] { "maTT" });
            DropIndex("dbo.NhanVien", new[] { "maVT" });
            DropIndex("dbo.ChuyenXes", new[] { "MaTuyenXe" });
            DropTable("dbo.PhongBan");
            DropTable("dbo.VaiTro");
            DropTable("dbo.TrangThaiNV");
            DropTable("dbo.TaiKhoanNV");
            DropTable("dbo.NhanVien");
            DropTable("dbo.TuyenXes");
            DropTable("dbo.ChuyenXes");
        }
    }
}
