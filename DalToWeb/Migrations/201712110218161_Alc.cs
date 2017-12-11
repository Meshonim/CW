namespace DalToWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Allocation",
                c => new
                    {
                        AllocationId = c.Int(nullable: false, identity: true),
                        AllocationValue = c.String(nullable: false, maxLength: 255),
                        AllocationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AllocationId)
                .Index(t => t.AllocationValue, unique: true);
            
            AddColumn("dbo.Exemplar", "AllocationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Exemplar", "AllocationId");
            AddForeignKey("dbo.Exemplar", "AllocationId", "dbo.Allocation", "AllocationId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Exemplar", "AllocationId", "dbo.Allocation");
            DropIndex("dbo.Exemplar", new[] { "AllocationId" });
            DropIndex("dbo.Allocation", new[] { "AllocationValue" });
            DropColumn("dbo.Exemplar", "AllocationId");
            DropTable("dbo.Allocation");
        }
    }
}
