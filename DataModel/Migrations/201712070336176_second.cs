namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChuyenXes",
                c => new
                    {
                        MaChuyenXe = c.Int(nullable: false, identity: true),
                        TenChuyenXe = c.String(),
                        NgayGioChay = c.DateTime(nullable: false),
                        TaiXe = c.Int(nullable: false),
                        TuyenXe_MaTuyenXe = c.Int(),
                    })
                .PrimaryKey(t => t.MaChuyenXe)
                .ForeignKey("dbo.TuyenXes", t => t.TuyenXe_MaTuyenXe)
                .Index(t => t.TuyenXe_MaTuyenXe);
            
            CreateTable(
                "dbo.TuyenXes",
                c => new
                    {
                        MaTuyenXe = c.Int(nullable: false, identity: true),
                        TenTuyenXe = c.String(),
                    })
                .PrimaryKey(t => t.MaTuyenXe);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChuyenXes", "TuyenXe_MaTuyenXe", "dbo.TuyenXes");
            DropIndex("dbo.ChuyenXes", new[] { "TuyenXe_MaTuyenXe" });
            DropTable("dbo.TuyenXes");
            DropTable("dbo.ChuyenXes");
        }
    }
}
