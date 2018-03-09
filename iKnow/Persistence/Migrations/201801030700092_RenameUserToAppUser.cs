using System.Data.Entity.Migrations;

namespace iKnow.Persistence.Migrations
{
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
