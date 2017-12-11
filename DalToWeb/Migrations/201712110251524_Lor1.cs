namespace DalToWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lor1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.LibraryOrders", newName: "LibraryOrder");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.LibraryOrder", newName: "LibraryOrders");
        }
    }
}
