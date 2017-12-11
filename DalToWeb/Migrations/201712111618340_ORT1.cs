namespace DalToWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ORT1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderReportTemplate", "TemplateName", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderReportTemplate", "TemplateName");
        }
    }
}
