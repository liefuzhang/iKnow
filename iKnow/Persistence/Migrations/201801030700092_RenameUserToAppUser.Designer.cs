// <auto-generated />

using System.CodeDom.Compiler;
using System.Data.Entity.Migrations.Infrastructure;
using System.Resources;

namespace iKnow.Persistence.Migrations
{
    [GeneratedCode("EntityFramework.Migrations", "6.2.0-61023")]
    public sealed partial class RenameUserToAppUser : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(RenameUserToAppUser));
        
        string IMigrationMetadata.Id
        {
            get { return "201801030700092_RenameUserToAppUser"; }
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
