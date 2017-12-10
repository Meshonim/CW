namespace DalToWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuthorFull : DbMigration
    {
        public override void Up()
        {
            Sql("ALTER TABLE dbo.Author ADD FullName AS AuthorFirstName + ' ' + AuthorMiddleName + ' ' + AuthorLastName");
        }
        
        public override void Down()
        {
            Sql("ALTER TABLE dbo.Author DROP COLUMN FullName");
        }
    }
}
