namespace iKnow.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsDeletedToQuestionAndAnswer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Questions", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "IsDeleted");
            DropColumn("dbo.Answers", "IsDeleted");
        }
    }
}
