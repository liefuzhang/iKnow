// <auto-generated />
namespace iKnow.Persistence.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.2.0-61023")]
    public sealed partial class AddIsDeletedToQuestionAndAnswer : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(AddIsDeletedToQuestionAndAnswer));
        
        string IMigrationMetadata.Id
        {
            get { return "201809142212596_AddIsDeletedToQuestionAndAnswer"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}