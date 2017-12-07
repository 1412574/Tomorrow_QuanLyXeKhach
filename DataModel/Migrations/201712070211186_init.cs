namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
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
            DropTable("dbo.PhongBan");
        }
    }
}
