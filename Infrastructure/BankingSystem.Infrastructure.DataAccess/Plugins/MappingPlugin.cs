using BankingSystem.Application.Models.Transactions;
using Itmo.Dev.Platform.Postgres.Plugins;
using Npgsql;

namespace BankingSystem.Infrastructure.DataAccess.Plugins;

public class MappingPlugin : IDataSourcePlugin
{
    public void Configure(NpgsqlDataSourceBuilder builder)
    {
        builder.MapEnum<OperationType>("transaction_type");
    }
}