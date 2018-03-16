namespace iKnow.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFollowingsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Followings",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        TopicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.TopicId })
                .ForeignKey("dbo.Topics", t => t.TopicId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TopicId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Followings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Followings", "TopicId", "dbo.Topics");
            DropIndex("dbo.Followings", new[] { "TopicId" });
            DropIndex("dbo.Followings", new[] { "UserId" });
            DropTable("dbo.Followings");
        }
    }
}
