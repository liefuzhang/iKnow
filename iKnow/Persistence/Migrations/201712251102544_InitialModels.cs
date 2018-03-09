using System.Data.Entity.Migrations;

namespace iKnow.Persistence.Migrations
{
    public partial class InitialModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
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
                        LoginName = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        RealName = c.String(),
                        Email = c.String(),
                        PhotoUrl = c.String(),
                        Intro = c.String(),
                        Gender = c.String(),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfiles", "Id", "dbo.Users");
            DropForeignKey("dbo.Questions", "UserId", "dbo.Users");
            DropIndex("dbo.UserProfiles", new[] { "Id" });
            DropIndex("dbo.Questions", new[] { "UserId" });
            DropTable("dbo.UserProfiles");
            DropTable("dbo.Users");
            DropTable("dbo.Questions");
        }
    }
}
