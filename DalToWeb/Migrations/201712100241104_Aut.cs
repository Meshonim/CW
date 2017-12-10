namespace DalToWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Aut : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Author",
                c => new
                    {
                        AuthorId = c.Int(nullable: false, identity: true),
                        AuthorFirstName = c.String(nullable: false),
                        AuthorMiddleName = c.String(),
                        AuthorLastName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AuthorId);
            
            CreateTable(
                "dbo.Edition_Author",
                c => new
                    {
                        EditionId = c.Int(nullable: false),
                        AuthorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EditionId, t.AuthorId })
                .ForeignKey("dbo.Edition", t => t.EditionId, cascadeDelete: true)
                .ForeignKey("dbo.Author", t => t.AuthorId, cascadeDelete: true)
                .Index(t => t.EditionId)
                .Index(t => t.AuthorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Edition_Author", "AuthorId", "dbo.Author");
            DropForeignKey("dbo.Edition_Author", "EditionId", "dbo.Edition");
            DropIndex("dbo.Edition_Author", new[] { "AuthorId" });
            DropIndex("dbo.Edition_Author", new[] { "EditionId" });
            DropTable("dbo.Edition_Author");
            DropTable("dbo.Author");
        }
    }
}
