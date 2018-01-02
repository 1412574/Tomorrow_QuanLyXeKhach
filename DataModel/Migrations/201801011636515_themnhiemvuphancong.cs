namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class themnhiemvuphancong : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PhanCongs", "nhiemVu", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PhanCongs", "nhiemVu");
        }
    }
}
