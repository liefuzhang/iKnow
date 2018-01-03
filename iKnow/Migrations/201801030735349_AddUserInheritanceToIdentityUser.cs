namespace iKnow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserInheritanceToIdentityUser : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AppUsers", newName: "AspNetUsers");
            DropForeignKey("dbo.UserProfiles", "Id", "dbo.AppUsers");
            DropForeignKey("dbo.Answers", "UserId", "dbo.AppUsers");
            DropForeignKey("dbo.Questions", "UserId", "dbo.AppUsers");
            DropForeignKey("dbo.TopicUsers", "UserId", "dbo.AppUsers");
            DropIndex("dbo.Answers", new[] { "UserId" });
            DropIndex("dbo.Questions", new[] { "UserId" });
            DropIndex("dbo.UserProfiles", new[] { "Id" });
            DropIndex("dbo.TopicUsers", new[] { "UserId" });
            RenameColumn(table: "dbo.Answers", name: "UserId", newName: "AppUserId");
            RenameColumn(table: "dbo.Questions", name: "UserId", newName: "AppUserId");
            DropPrimaryKey("dbo.AspNetUsers");
            DropPrimaryKey("dbo.TopicUsers");
            DropColumn("dbo.AspNetUsers", "Id");
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            AddColumn("dbo.AspNetUsers", "Email", c => c.String(maxLength: 256));
            AddColumn("dbo.AspNetUsers", "EmailConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "PasswordHash", c => c.String());
            AddColumn("dbo.AspNetUsers", "SecurityStamp", c => c.String());
            AddColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String());
            AddColumn("dbo.AspNetUsers", "PhoneNumberConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "TwoFactorEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "LockoutEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "AccessFailedCount", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "UserName", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.AspNetUsers", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Answers", "AppUserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Questions", "AppUserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.TopicUsers", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.AspNetUsers", "Id");
            AddPrimaryKey("dbo.TopicUsers", new[] { "UserId", "TopicId" });
            CreateIndex("dbo.Answers", "AppUserId");
            CreateIndex("dbo.AspNetUsers", "UserName", unique: true, name: "UserNameIndex");
            CreateIndex("dbo.Questions", "AppUserId");
            CreateIndex("dbo.TopicUsers", "UserId");
            AddForeignKey("dbo.Answers", "AppUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Questions", "AppUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.TopicUsers", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.AspNetUsers", "LoginName");
            DropTable("dbo.UserProfiles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        RealName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 255),
                        PhotoUrl = c.String(),
                        Intro = c.String(),
                        Gender = c.String(nullable: false),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "LoginName", c => c.String(nullable: false, maxLength: 255));
            DropForeignKey("dbo.TopicUsers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Questions", "AppUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Answers", "AppUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.TopicUsers", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Questions", new[] { "AppUserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Answers", new[] { "AppUserId" });
            DropPrimaryKey("dbo.TopicUsers");
            DropPrimaryKey("dbo.AspNetUsers");
            AlterColumn("dbo.TopicUsers", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Questions", "AppUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Answers", "AppUserId", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "Id");
            DropColumn("dbo.AspNetUsers", "UserName");
            DropColumn("dbo.AspNetUsers", "AccessFailedCount");
            DropColumn("dbo.AspNetUsers", "LockoutEnabled");
            DropColumn("dbo.AspNetUsers", "LockoutEndDateUtc");
            DropColumn("dbo.AspNetUsers", "TwoFactorEnabled");
            DropColumn("dbo.AspNetUsers", "PhoneNumberConfirmed");
            DropColumn("dbo.AspNetUsers", "PhoneNumber");
            DropColumn("dbo.AspNetUsers", "SecurityStamp");
            DropColumn("dbo.AspNetUsers", "PasswordHash");
            DropColumn("dbo.AspNetUsers", "EmailConfirmed");
            DropColumn("dbo.AspNetUsers", "Email");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            AddColumn("dbo.AspNetUsers", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.TopicUsers", new[] { "UserId", "TopicId" });
            AddPrimaryKey("dbo.AspNetUsers", "Id");
            RenameColumn(table: "dbo.Questions", name: "AppUserId", newName: "UserId");
            RenameColumn(table: "dbo.Answers", name: "AppUserId", newName: "UserId");
            CreateIndex("dbo.TopicUsers", "UserId");
            CreateIndex("dbo.UserProfiles", "Id");
            CreateIndex("dbo.Questions", "UserId");
            CreateIndex("dbo.Answers", "UserId");
            AddForeignKey("dbo.TopicUsers", "UserId", "dbo.AppUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Questions", "UserId", "dbo.AppUsers", "Id");
            AddForeignKey("dbo.Answers", "UserId", "dbo.AppUsers", "Id");
            AddForeignKey("dbo.UserProfiles", "Id", "dbo.AppUsers", "Id");
            RenameTable(name: "dbo.AspNetUsers", newName: "AppUsers");
        }
    }
}
