namespace iKnow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUpdatedDateToAnswer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "UpdatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Answers", "UpdatedDate");
        }
    }
}
