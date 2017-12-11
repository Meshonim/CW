namespace DalToWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rule1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rule",
                c => new
                    {
                        RuleId = c.Int(nullable: false, identity: true),
                        RuleValue = c.String(nullable: false, maxLength: 300),
                        RulePriority = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.RuleId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Rule");
        }
    }
}
