using Dapper;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace MyRecipeBook.Infrastructure.Migrations;

public static class DataBaseMigration
{
    public static void Migrate(string ConnectionString, IServiceProvider serviceProvider)
    {
        EnsureDatabaseCreated(ConnectionString);

        MigrationDataBase(serviceProvider);
    }

    private static void EnsureDatabaseCreated(string ConnectionString)
    {
        var connectionStringBuilder = new SqlConnectionStringBuilder(ConnectionString);

        var dataBaseName = connectionStringBuilder.InitialCatalog;

        connectionStringBuilder.Remove("Database");

        using var dbconnection = new SqlConnection(connectionStringBuilder.ConnectionString);

       var parameters = new DynamicParameters();
        parameters.Add("name", dataBaseName);

        var records = dbconnection.Query("SELECT * FROM sys.databases WHERE name = @name", parameters);

        if (records.Any() == false )
        {
            dbconnection.Execute($"CREATE DATABASE [{dataBaseName}]");
        }


    }

    private static void MigrationDataBase(IServiceProvider serviceProvider)
    {
       var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

        runner.ListMigrations();

        runner.MigrateUp();
    }
}
