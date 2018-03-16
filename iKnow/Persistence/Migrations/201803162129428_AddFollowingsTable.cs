namespace iKnow.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFollowingsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TopicFollowings",
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
            DropForeignKey("dbo.TopicFollowings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TopicFollowings", "TopicId", "dbo.Topics");
            DropIndex("dbo.TopicFollowings", new[] { "TopicId" });
            DropIndex("dbo.TopicFollowings", new[] { "UserId" });
            DropTable("dbo.TopicFollowings");
        }
    }
}
