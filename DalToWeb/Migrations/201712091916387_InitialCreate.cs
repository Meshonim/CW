namespace DalToWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.City",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        CityName = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.CityId)
                .Index(t => t.CityName, unique: true);
            
            CreateTable(
                "dbo.House",
                c => new
                    {
                        HouseId = c.Int(nullable: false, identity: true),
                        HouseName = c.String(nullable: false, maxLength: 255),
                        Phone = c.String(maxLength: 20),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HouseId)
                .ForeignKey("dbo.City", t => t.CityId, cascadeDelete: true)
                .Index(t => new { t.HouseName, t.CityId }, unique: true, name: "IX_HouseNameCityId");
            
            CreateTable(
                "dbo.Edition",
                c => new
                    {
                        EditionId = c.Int(nullable: false, identity: true),
                        EditionTitle = c.String(nullable: false, maxLength: 100),
                        EditionYear = c.Short(nullable: false),
                        HouseId = c.Int(nullable: false),
                        LanguageId = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.EditionId)
                .ForeignKey("dbo.Language", t => t.LanguageId, cascadeDelete: true)
                .ForeignKey("dbo.House", t => t.HouseId, cascadeDelete: true)
                .Index(t => t.HouseId)
                .Index(t => t.LanguageId);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        GenreId = c.Short(nullable: false, identity: true),
                        GenreName = c.String(nullable: false, maxLength: 45),
                    })
                .PrimaryKey(t => t.GenreId)
                .Index(t => t.GenreName, unique: true);
            
            CreateTable(
                "dbo.Language",
                c => new
                    {
                        LanguageId = c.Short(nullable: false, identity: true),
                        LanguageName = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.LanguageId)
                .Index(t => t.LanguageName, unique: true);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Age = c.Int(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        RoleId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Edition_Genre",
                c => new
                    {
                        EditionId = c.Int(nullable: false),
                        GenreId = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.EditionId, t.GenreId })
                .ForeignKey("dbo.Edition", t => t.EditionId, cascadeDelete: true)
                .ForeignKey("dbo.Genre", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.EditionId)
                .Index(t => t.GenreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Profiles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.House", "CityId", "dbo.City");
            DropForeignKey("dbo.Edition", "HouseId", "dbo.House");
            DropForeignKey("dbo.Edition", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.Edition_Genre", "GenreId", "dbo.Genre");
            DropForeignKey("dbo.Edition_Genre", "EditionId", "dbo.Edition");
            DropIndex("dbo.Edition_Genre", new[] { "GenreId" });
            DropIndex("dbo.Edition_Genre", new[] { "EditionId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Profiles", new[] { "UserId" });
            DropIndex("dbo.Language", new[] { "LanguageName" });
            DropIndex("dbo.Genre", new[] { "GenreName" });
            DropIndex("dbo.Edition", new[] { "LanguageId" });
            DropIndex("dbo.Edition", new[] { "HouseId" });
            DropIndex("dbo.House", "IX_HouseNameCityId");
            DropIndex("dbo.City", new[] { "CityName" });
            DropTable("dbo.Edition_Genre");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Profiles");
            DropTable("dbo.Language");
            DropTable("dbo.Genre");
            DropTable("dbo.Edition");
            DropTable("dbo.House");
            DropTable("dbo.City");
        }
    }
}
