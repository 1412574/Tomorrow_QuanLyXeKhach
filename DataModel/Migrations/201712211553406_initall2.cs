namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initall2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.KhachHangs", "soDienThoai", c => c.String(nullable: false, maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.KhachHangs", "soDienThoai", c => c.String(maxLength: 15));
        }
    }
}
