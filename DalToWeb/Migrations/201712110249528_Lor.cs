namespace DalToWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LibraryOrders",
                c => new
                    {
                        LibraryOrderId = c.Int(nullable: false, identity: true),
                        LibraryOrderStatus = c.Boolean(nullable: false),
                        LibraryOrderCount = c.Int(nullable: false),
                        EditionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LibraryOrderId)
                .ForeignKey("dbo.Edition", t => t.EditionId, cascadeDelete: true)
                .Index(t => t.EditionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LibraryOrders", "EditionId", "dbo.Edition");
            DropIndex("dbo.LibraryOrders", new[] { "EditionId" });
            DropTable("dbo.LibraryOrders");
        }
    }
}
