namespace DalToWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Edition", "EditionImage", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Edition", "EditionImage");
        }
    }
}
