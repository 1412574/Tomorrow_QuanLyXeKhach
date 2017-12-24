namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BaoCaoinit : DbMigration
    {
        public override void Up()
        {
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
            DropForeignKey("dbo.ChuyenXes", "MaTuyenXe", "dbo.TuyenXes");
            DropIndex("dbo.ChuyenXes", new[] { "MaTuyenXe" });
            DropTable("dbo.PhongBan");
            DropTable("dbo.TuyenXes");
            DropTable("dbo.ChuyenXes");
            DropTable("dbo.BaoCao");
        }
    }
}
