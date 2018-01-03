namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class themLopChamCong : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BangChamCongs",
                c => new
                    {
                        maCC = c.Int(nullable: false, identity: true),
                        maNV = c.Int(nullable: false),
                        ngay = c.Int(nullable: false),
                        gioBatDau = c.Int(nullable: false),
                        gioKetThuc = c.Int(),
                        ghiChu = c.String(),
                    })
                .PrimaryKey(t => t.maCC)
                .ForeignKey("dbo.NhanVien", t => t.maNV, cascadeDelete: true)
                .Index(t => t.maNV);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BangChamCongs", "maNV", "dbo.NhanVien");
            DropIndex("dbo.BangChamCongs", new[] { "maNV" });
            DropTable("dbo.BangChamCongs");
        }
    }
}
