using PostCodeSearch.Models.Persistence;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Infrastructure.Migrations;

namespace PostCodeSearch.Migration
{
    public class AddPostCodesTable : MigrationBase
    {
        private readonly IMigrationContext context;

        public AddPostCodesTable(IMigrationContext context) : base(context)
        {
            this.context = context;
        }

        protected override void Migrate()
        {
            Logger.LogDebug("Running migration {MigrationStep}", nameof(AddPostCodesTable));

            // Lots of methods available in the MigrationBase class - discover with this.
            if (!TableExists(PostCodes.TableName))
            {
                Create.Table<PostCodes>().Do();
            }
            else
            {
                Logger.LogDebug("The database table {DbTable} already exists, skipping", "BlogComments");
            }
        }
    }
}
