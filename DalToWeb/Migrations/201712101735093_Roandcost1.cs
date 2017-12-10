namespace DalToWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Roandcost1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReaderOrder",
                c => new
                    {
                        ReaderOrderId = c.Int(nullable: false, identity: true),
                        ReaderOrderDateOfIssue = c.DateTime(nullable: false),
                        ReaderOrderExpiryDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        ExemplarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReaderOrderId)
                .ForeignKey("dbo.Exemplar", t => t.ExemplarId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ExemplarId);
            
            AddColumn("dbo.Exemplar", "ExemplarCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Exemplar", "Cost");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Exemplar", "Cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.ReaderOrder", "UserId", "dbo.Users");
            DropForeignKey("dbo.ReaderOrder", "ExemplarId", "dbo.Exemplar");
            DropIndex("dbo.ReaderOrder", new[] { "ExemplarId" });
            DropIndex("dbo.ReaderOrder", new[] { "UserId" });
            DropColumn("dbo.Exemplar", "ExemplarCost");
            DropTable("dbo.ReaderOrder");
        }
    }
}
