using BankingSystem.Application.Models.Transactions;
using BankingSystem.Application.Abstractions.Repositories;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace BankingSystem.Infrastructure.DataAccess.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public TransactionRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public IEnumerable<Operation> GetTransactions(long accountId)
    {
        const string sql = """
                           select transaction_type, amount
                           from transactions
                           where account_id = :accountId
                           order by transaction_id desc;
                           """;
        using NpgsqlConnection? connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("accountId", accountId);

        using NpgsqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            yield return new Operation(
                Type: reader.GetFieldValue<OperationType>(0),
                Amount: reader.GetInt32(1));
        }
    }

    public void AddTransaction(Operation operation, long accountId)
    {
        const string sql = """
                           insert into transactions(account_id, transaction_type, amount)
                           values (:accountId, :transaction_type, :amount)
                           """;
        using NpgsqlConnection? connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("accountId", accountId)
            .AddParameter("transaction_type", operation.Type)
            .AddParameter("amount", operation.Amount);

        command.ExecuteNonQuery();
    }
}