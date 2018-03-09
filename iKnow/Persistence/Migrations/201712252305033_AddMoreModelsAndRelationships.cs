using System.Data.Entity.Migrations;

namespace iKnow.Persistence.Migrations
{
    public partial class AddMoreModelsAndRelationships : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "UserId", "dbo.Users");
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.QuestionId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TopicUsers",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        TopicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.TopicId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Topics", t => t.TopicId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TopicId);
            
            CreateTable(
                "dbo.TopicQuestions",
                c => new
                    {
                        TopicId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TopicId, t.QuestionId })
                .ForeignKey("dbo.Questions", t => t.TopicId, cascadeDelete: true)
                .ForeignKey("dbo.Topics", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.TopicId)
                .Index(t => t.QuestionId);
            
            AlterColumn("dbo.Questions", "Title", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Questions", "Description", c => c.String(maxLength: 1000));
            AlterColumn("dbo.UserProfiles", "RealName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.UserProfiles", "Email", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.UserProfiles", "Gender", c => c.String(nullable: false));
            AddForeignKey("dbo.Questions", "UserId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "UserId", "dbo.Users");
            DropForeignKey("dbo.TopicQuestions", "QuestionId", "dbo.Topics");
            DropForeignKey("dbo.TopicQuestions", "TopicId", "dbo.Questions");
            DropForeignKey("dbo.TopicUsers", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.TopicUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.Answers", "UserId", "dbo.Users");
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropIndex("dbo.TopicQuestions", new[] { "QuestionId" });
            DropIndex("dbo.TopicQuestions", new[] { "TopicId" });
            DropIndex("dbo.TopicUsers", new[] { "TopicId" });
            DropIndex("dbo.TopicUsers", new[] { "UserId" });
            DropIndex("dbo.Answers", new[] { "UserId" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            AlterColumn("dbo.UserProfiles", "Gender", c => c.String());
            AlterColumn("dbo.UserProfiles", "Email", c => c.String());
            AlterColumn("dbo.UserProfiles", "RealName", c => c.String());
            AlterColumn("dbo.Questions", "Description", c => c.String());
            AlterColumn("dbo.Questions", "Title", c => c.String());
            DropTable("dbo.TopicQuestions");
            DropTable("dbo.TopicUsers");
            DropTable("dbo.Topics");
            DropTable("dbo.Answers");
            AddForeignKey("dbo.Questions", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
