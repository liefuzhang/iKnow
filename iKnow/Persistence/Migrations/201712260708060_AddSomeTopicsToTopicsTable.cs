using System.Data.Entity.Migrations;

namespace iKnow.Persistence.Migrations
{
    public partial class AddSomeTopicsToTopicsTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Topics (Name, Description) VALUES ('Software Engineering', 'Software Engineering is the application of engineering to the development of software in a systematic method.')");
            Sql("INSERT INTO Topics (Name, Description) VALUES ('New Zealand', 'New Zealand is an island country in the South Pacific. It is one of the most remote countries in the world, and was the last major landmass to be settled by European humans.')");
            Sql("INSERT INTO Topics (Name, Description) VALUES ('Internet', 'The Internet is the global system of interconnected computer networks that use the Internet protocol suite (TCP/IP) to link devices worldwide.')");
        }

        public override void Down()
        {
        }
    }
}
