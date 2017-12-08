namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
                "dbo.PhongBan",
                c => new
                    {
                        tenPB = c.String(nullable: false, maxLength: 10),
                        maPB = c.Int(nullable: false, identity: true),
                        moTaPB = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.tenPB);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChuyenXes", "MaTuyenXe", "dbo.TuyenXes");
            DropIndex("dbo.ChuyenXes", new[] { "MaTuyenXe" });
            DropTable("dbo.PhongBan");
            DropTable("dbo.TuyenXes");
            DropTable("dbo.ChuyenXes");
        }
    }
}
