using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.Users;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Infrastructure.DataAccess.Repositories;
using MyRecipeBook.Infrastructure.Extensions;
using System.Reflection;

namespace MyRecipeBook.Infrastructure;

public static class DepencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.ConnectionString();

        AddRepositories(services);
        AddDbCobtext(services, connectionString!);
        AddFluentMigrator(services, configuration);
    }

    public static void AddDbCobtext(IServiceCollection services, string connectionString)
    {
        services.AddDbContext<MyRecipeBookDbCobtext>(dbContextOptions =>
        {
            dbContextOptions.UseSqlServer(connectionString);
        });
    }

    public static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentMigratorCore()
            .ConfigureRunner(options => options
                .AddSqlServer()
                .WithGlobalConnectionString(configuration.ConnectionString())
                .ScanIn(Assembly.Load("MyRecipeBook.Infrastructure")).For.All());
    }
}
