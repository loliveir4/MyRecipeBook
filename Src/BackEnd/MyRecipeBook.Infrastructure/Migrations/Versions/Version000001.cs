using FluentMigrator;

namespace MyRecipeBook.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_USER, "Create table to save the user's information")]
public class Version000001 : VersionBase
{
    public override void Up()
    {
        CreateTable("User")
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("Email").AsString(255).NotNullable()
            .WithColumn("Password").AsString(2000).NotNullable();
    }
}
