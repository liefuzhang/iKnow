namespace iKnow.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class FixImageLinksInAnswers : DbMigration
    {
        public override void Up()
        {
            Sql(@"

SET NOCOUNT ON;

	DECLARE @NumberRecords INT;
	DECLARE @aid INT;
	DECLARE @content NVARCHAR(MAX);

	DECLARE @AnswersToUpdate table
	(		
		Id INT IDENTITY(1,1),
		AnswerId INT
	)

	INSERT INTO @AnswersToUpdate(AnswerId) SELECT Id FROM Answers Where Content like '%https://qph.ec.quoracdn.net%'

	SET @NumberRecords = @@ROWCOUNT
	
	DECLARE @RowIndex INT = 1

	WHILE(@RowIndex <= @NumberRecords)
	BEGIN		

			SELECT @aid = AnswerId FROM @AnswersToUpdate WHERE Id = @RowIndex
			SELECT @content = Content FROM Answers WHERE Id=@aid

			SET @content = REPLACE(@content,'https://qph.ec.quoracdn.net','https://qph.fs.quoracdn.net')
			UPDATE Answers SET Content = @content WHERE Id = @aid

			SET @RowIndex = @RowIndex + 1	
	END									

                ");
        }


        public override void Down()
        {
        }
    }
}
