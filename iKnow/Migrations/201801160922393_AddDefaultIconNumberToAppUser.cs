namespace iKnow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDefaultIconNumberToAppUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DefaultIconNumber", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DefaultIconNumber");
        }
    }
}
