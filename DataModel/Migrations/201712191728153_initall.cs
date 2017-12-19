namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initall : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DatVes", "maChuyenXe", "dbo.ChuyenXes");
            DropIndex("dbo.DatVes", new[] { "maChuyenXe" });
            AlterColumn("dbo.ChiTietDatVes", "soGhe", c => c.Int(nullable: false));
            AlterColumn("dbo.DatVes", "maChuyenXe", c => c.Int(nullable: false));
            CreateIndex("dbo.DatVes", "maChuyenXe");
            AddForeignKey("dbo.DatVes", "maChuyenXe", "dbo.ChuyenXes", "MaChuyenXe", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DatVes", "maChuyenXe", "dbo.ChuyenXes");
            DropIndex("dbo.DatVes", new[] { "maChuyenXe" });
            AlterColumn("dbo.DatVes", "maChuyenXe", c => c.Int());
            AlterColumn("dbo.ChiTietDatVes", "soGhe", c => c.Int());
            CreateIndex("dbo.DatVes", "maChuyenXe");
            AddForeignKey("dbo.DatVes", "maChuyenXe", "dbo.ChuyenXes", "MaChuyenXe");
        }
    }
}
