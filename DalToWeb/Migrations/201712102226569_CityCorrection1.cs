namespace DalToWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CityCorrection1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.City", "CityPopulation", c => c.Int(nullable: false));
            DropColumn("dbo.City", "Population");
        }
        
        public override void Down()
        {
            AddColumn("dbo.City", "Population", c => c.Int(nullable: false));
            DropColumn("dbo.City", "CityPopulation");
        }
    }
}
