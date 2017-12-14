namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initall : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DatVes",
                c => new
                    {
                        maDatVe = c.Int(nullable: false, identity: true),
                        maChuyenXe = c.Int(),
                        soGhe = c.String(maxLength: 5),
                        giaVe = c.Int(),
                        trangThai = c.String(maxLength: 20),
                        maKhachHang = c.Int(),
                    })
                .PrimaryKey(t => t.maDatVe)
                .ForeignKey("dbo.ChuyenXes", t => t.maChuyenXe)
                .ForeignKey("dbo.KhachHangs", t => t.maKhachHang)
                .Index(t => t.maChuyenXe)
                .Index(t => t.maKhachHang);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DatVes", "maKhachHang", "dbo.KhachHangs");
            DropForeignKey("dbo.DatVes", "maChuyenXe", "dbo.ChuyenXes");
            DropIndex("dbo.DatVes", new[] { "maKhachHang" });
            DropIndex("dbo.DatVes", new[] { "maChuyenXe" });
            DropTable("dbo.KhachHangs");
            DropTable("dbo.DatVes");
        }
    }
}
