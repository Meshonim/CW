namespace DalToWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Profiles", "UserId", "dbo.Users");
            DropIndex("dbo.Profiles", new[] { "UserId" });
            DropTable("dbo.Profiles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Age = c.Int(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Profiles", "UserId");
            AddForeignKey("dbo.Profiles", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
