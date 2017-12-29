namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDB : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TuyenXes", newName: "TuyenXe");
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
            
            AddColumn("dbo.TuyenXe", "LoaiXe", c => c.String(maxLength: 50));
            AddColumn("dbo.TuyenXe", "ThoiGian", c => c.Int(nullable: false));
            AddColumn("dbo.TuyenXe", "QuangDuong", c => c.Int(nullable: false));
            AddColumn("dbo.TuyenXe", "SoChuyen", c => c.Int(nullable: false));
            AlterColumn("dbo.TuyenXe", "TenTuyenXe", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UngVien", "TrangThaiUV_maTT", "dbo.TrangThaiUV");
            DropForeignKey("dbo.UngVien", "maLPV", "dbo.LichPhongVan");
            DropForeignKey("dbo.HanhTrinhs", "MaTuyenXe", "dbo.TuyenXe");
            DropForeignKey("dbo.HanhTrinhs", "MaTramXe", "dbo.TramXes");
            DropIndex("dbo.UngVien", new[] { "TrangThaiUV_maTT" });
            DropIndex("dbo.UngVien", new[] { "maLPV" });
            DropIndex("dbo.HanhTrinhs", new[] { "MaTramXe" });
            DropIndex("dbo.HanhTrinhs", new[] { "MaTuyenXe" });
            AlterColumn("dbo.TuyenXe", "TenTuyenXe", c => c.String());
            DropColumn("dbo.TuyenXe", "SoChuyen");
            DropColumn("dbo.TuyenXe", "QuangDuong");
            DropColumn("dbo.TuyenXe", "ThoiGian");
            DropColumn("dbo.TuyenXe", "LoaiXe");
            DropTable("dbo.ThongKe");
            DropTable("dbo.TrangThaiUV");
            DropTable("dbo.UngVien");
            DropTable("dbo.LichPhongVan");
            DropTable("dbo.TramXes");
            DropTable("dbo.HanhTrinhs");
            DropTable("dbo.BaoCao");
            RenameTable(name: "dbo.TuyenXe", newName: "TuyenXes");
        }
    }
}
