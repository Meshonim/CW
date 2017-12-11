namespace DalToWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Illustrators1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Edition_Illustrator",
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
            DropForeignKey("dbo.Edition_Illustrator", "AuthorId", "dbo.Author");
            DropForeignKey("dbo.Edition_Illustrator", "EditionId", "dbo.Edition");
            DropIndex("dbo.Edition_Illustrator", new[] { "AuthorId" });
            DropIndex("dbo.Edition_Illustrator", new[] { "EditionId" });
            DropTable("dbo.Edition_Illustrator");
        }
    }
}
