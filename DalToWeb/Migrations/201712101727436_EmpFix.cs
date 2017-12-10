namespace DalToWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmpFix : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Exemplars", newName: "Exemplar");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Exemplar", newName: "Exemplars");
        }
    }
}
