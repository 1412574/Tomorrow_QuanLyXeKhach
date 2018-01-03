namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class capnhatLopChamCong1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BangChamCongs", "ngay", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BangChamCongs", "gioBatDau", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BangChamCongs", "gioKetThuc", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BangChamCongs", "gioKetThuc", c => c.Int(nullable: false));
            AlterColumn("dbo.BangChamCongs", "gioBatDau", c => c.Int(nullable: false));
            AlterColumn("dbo.BangChamCongs", "ngay", c => c.Int(nullable: false));
        }
    }
}
