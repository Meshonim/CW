namespace DalToWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CityCorrection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.City", "Population", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.City", "Population");
        }
    }
}
