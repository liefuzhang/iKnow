namespace iKnow.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameFollowingTableToTopicFollowing : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Followings", newName: "TopicFollowings");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.TopicFollowings", newName: "Followings");
        }
    }
}
