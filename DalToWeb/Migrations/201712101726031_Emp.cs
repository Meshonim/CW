namespace DalToWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Emp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exemplars",
                c => new
                    {
                        ExemplarId = c.Int(nullable: false, identity: true),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EditionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExemplarId)
                .ForeignKey("dbo.Edition", t => t.EditionId, cascadeDelete: true)
                .Index(t => t.EditionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Exemplars", "EditionId", "dbo.Edition");
            DropIndex("dbo.Exemplars", new[] { "EditionId" });
            DropTable("dbo.Exemplars");
        }
    }
}
