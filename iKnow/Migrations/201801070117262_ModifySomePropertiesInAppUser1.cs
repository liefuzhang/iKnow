namespace iKnow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifySomePropertiesInAppUser1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Intro", c => c.String(maxLength: 255));
            AddColumn("dbo.AspNetUsers", "Gender", c => c.Byte(nullable: false));
            AddColumn("dbo.AspNetUsers", "Location", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Location");
            DropColumn("dbo.AspNetUsers", "Gender");
            DropColumn("dbo.AspNetUsers", "Intro");
        }
    }
}
