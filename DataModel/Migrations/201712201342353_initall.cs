namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initall : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DatVes", "trangThai", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DatVes", "trangThai", c => c.String(maxLength: 20));
        }
    }
}
