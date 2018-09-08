namespace iKnow.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeCommentReplyToAsOptional : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Comments", new[] { "ReplyToCommentId" });
            AlterColumn("dbo.Comments", "ReplyToCommentId", c => c.Int());
            CreateIndex("dbo.Comments", "ReplyToCommentId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Comments", new[] { "ReplyToCommentId" });
            AlterColumn("dbo.Comments", "ReplyToCommentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "ReplyToCommentId");
        }
    }
}
