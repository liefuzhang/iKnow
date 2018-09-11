namespace iKnow.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAnswerLikesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnswerLikes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppUserId = c.String(maxLength: 128),
                        AnswerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Answers", t => t.AnswerId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.AppUserId)
                .Index(t => t.AppUserId)
                .Index(t => t.AnswerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnswerLikes", "AppUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AnswerLikes", "AnswerId", "dbo.Answers");
            DropIndex("dbo.AnswerLikes", new[] { "AnswerId" });
            DropIndex("dbo.AnswerLikes", new[] { "AppUserId" });
            DropTable("dbo.AnswerLikes");
        }
    }
}
