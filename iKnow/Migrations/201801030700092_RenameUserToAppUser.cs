namespace iKnow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameUserToAppUser : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Users", newName: "AppUsers");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.AppUsers", newName: "Users");
        }
    }
}
