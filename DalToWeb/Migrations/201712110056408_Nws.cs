namespace DalToWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nws : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                    {
                        NewsId = c.Int(nullable: false, identity: true),
                        NewsTitle = c.String(nullable: false, maxLength: 100),
                        NewsCreationDate = c.DateTime(nullable: false),
                        NewsContent = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.NewsId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.News");
        }
    }
}
