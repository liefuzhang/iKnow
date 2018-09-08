namespace iKnow.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommentsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        AnswerId = c.Int(nullable: false),
                        AppUserId = c.String(maxLength: 128),
                        ReplyToCommentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Answers", t => t.AnswerId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.AppUserId)
                .ForeignKey("dbo.Comments", t => t.ReplyToCommentId)
                .Index(t => t.AnswerId)
                .Index(t => t.AppUserId)
                .Index(t => t.ReplyToCommentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "ReplyToCommentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "AppUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "AnswerId", "dbo.Answers");
            DropIndex("dbo.Comments", new[] { "ReplyToCommentId" });
            DropIndex("dbo.Comments", new[] { "AppUserId" });
            DropIndex("dbo.Comments", new[] { "AnswerId" });
            DropTable("dbo.Comments");
        }
    }
}
