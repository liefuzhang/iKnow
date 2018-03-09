using System.Data.Entity.Migrations;

namespace iKnow.Persistence.Migrations {
    public partial class AddSeedTopic : DbMigration {
        public override void Up() {
            Sql("INSERT INTO [dbo].[Topics] ([Name], [Description]) VALUES (N'Software Engineering', N'Software engineering is the process of analyzing user needs and designing, constructing, and testing end user applications that will satisfy these needs through the use of software programming languages. It is the application of engineering principles to software development. In contrast to simple programming, software engineering is used for larger and more complex software systems, which are used as critical systems for businesses and organizations.')");
        }

        public override void Down() {
        }
    }
}
