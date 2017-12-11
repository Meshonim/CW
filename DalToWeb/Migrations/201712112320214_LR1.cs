namespace DalToWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LR1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LibraryReportTemplate",
                c => new
                    {
                        LibraryReportTemplateId = c.Int(nullable: false, identity: true),
                        TemplateName = c.String(nullable: false, maxLength: 255),
                        PrintHeaders = c.Boolean(nullable: false),
                        Autosized = c.Boolean(nullable: false),
                        MaxCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LibraryReportTemplateId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LibraryReportTemplate");
        }
    }
}
