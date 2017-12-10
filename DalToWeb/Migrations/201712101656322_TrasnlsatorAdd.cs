namespace DalToWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TrasnlsatorAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Edition_Translator",
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
            DropForeignKey("dbo.Edition_Translator", "AuthorId", "dbo.Author");
            DropForeignKey("dbo.Edition_Translator", "EditionId", "dbo.Edition");
            DropIndex("dbo.Edition_Translator", new[] { "AuthorId" });
            DropIndex("dbo.Edition_Translator", new[] { "EditionId" });
            DropTable("dbo.Edition_Translator");
        }
    }
}
