namespace DalToWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ORT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderReportTemplate",
                c => new
                    {
                        OrderReportTemplateId = c.Int(nullable: false, identity: true),
                        PrintBackground = c.Boolean(nullable: false),
                        PrintDayOfWeek = c.Boolean(nullable: false),
                        PrintEditionTitle = c.Boolean(nullable: false),
                        MaxCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderReportTemplateId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OrderReportTemplate");
        }
    }
}
