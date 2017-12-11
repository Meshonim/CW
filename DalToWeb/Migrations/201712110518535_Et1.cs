namespace DalToWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Et1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EditionType",
                c => new
                    {
                        EditionTypeId = c.Int(nullable: false, identity: true),
                        EditionTypeName = c.String(nullable: false, maxLength: 50),
                        EditionTypeDescription = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.EditionTypeId);
            
            AddColumn("dbo.Edition", "EditionTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Edition", "EditionTypeId");
            AddForeignKey("dbo.Edition", "EditionTypeId", "dbo.EditionType", "EditionTypeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Edition", "EditionTypeId", "dbo.EditionType");
            DropIndex("dbo.Edition", new[] { "EditionTypeId" });
            DropColumn("dbo.Edition", "EditionTypeId");
            DropTable("dbo.EditionType");
        }
    }
}
