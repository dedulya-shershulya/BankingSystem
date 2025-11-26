using Itmo.Dev.Platform.Common.Extensions;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.Dev.Platform.Postgres.Models;
using Itmo.Dev.Platform.Postgres.Plugins;
using BankingSystem.Application.Abstractions.Repositories;
using BankingSystem.Infrastructure.DataAccess.Plugins;
using BankingSystem.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BankingSystem.Infrastructure.DataAccess.Extension;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureDataAccess(
        this IServiceCollection collection,
        Action<PostgresConnectionConfiguration> configuration)
    {
        collection.AddPlatform();
        collection.AddPlatformPostgres(builder => builder.Configure(configuration));
        collection.AddPlatformMigrations(typeof(ServiceCollectionExtensions).Assembly);

        collection.AddSingleton<IDataSourcePlugin, MappingPlugin>();

        collection.AddScoped<IBankAccountRepository, BankAccountRepository>();
        collection.AddScoped<ITransactionRepository, TransactionRepository>();
        collection.AddScoped<IAdminPasswordRepository, AdminPasswordRepository>();

        return collection;
    }
}