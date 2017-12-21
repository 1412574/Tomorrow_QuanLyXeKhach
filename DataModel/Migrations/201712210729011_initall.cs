namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initall : DbMigration
    {
        public override void Up()
        {
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
                .ForeignKey("dbo.TuyenXes", t => t.MaTuyenXe, cascadeDelete: true)
                .Index(t => t.MaTuyenXe);
            
            CreateTable(
                "dbo.TuyenXes",
                c => new
                    {
                        MaTuyenXe = c.Int(nullable: false, identity: true),
                        TenTuyenXe = c.String(),
                        GiaVe = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaTuyenXe);
            
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
            DropForeignKey("dbo.DatVes", "maKhachHang", "dbo.KhachHangs");
            DropForeignKey("dbo.ChuyenXes", "MaTuyenXe", "dbo.TuyenXes");
            DropForeignKey("dbo.DatVes", "maChuyenXe", "dbo.ChuyenXes");
            DropForeignKey("dbo.ChiTietDatVes", "maDatVe", "dbo.DatVes");
            DropIndex("dbo.ChuyenXes", new[] { "MaTuyenXe" });
            DropIndex("dbo.DatVes", new[] { "maChuyenXe" });
            DropIndex("dbo.DatVes", new[] { "maKhachHang" });
            DropIndex("dbo.ChiTietDatVes", new[] { "maDatVe" });
            DropTable("dbo.PhongBan");
            DropTable("dbo.KhachHangs");
            DropTable("dbo.TuyenXes");
            DropTable("dbo.ChuyenXes");
            DropTable("dbo.DatVes");
            DropTable("dbo.ChiTietDatVes");
        }
    }
}
