using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using BankingSystem.Application.Abstractions.Repositories;
using BankingSystem.Application.Models.BankAccounts;
using Npgsql;

namespace BankingSystem.Infrastructure.DataAccess.Repositories;

public class BankAccountRepository : IBankAccountRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public BankAccountRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public BankAccount? FindBankAccountById(long accountId)
    {
        const string sql = """
                           select account_id, account_pin, balance
                           from bank_accounts
                           where account_id = :account_id;
                           """;

        using NpgsqlConnection? connection = _connectionProvider
            .GetConnectionAsync(CancellationToken.None)
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("account_id", accountId);

        using NpgsqlDataReader reader = command.ExecuteReader();

        if (reader.Read() is false)
            return null;

        return new BankAccount(
            Id: reader.GetInt64(0),
            Pin: reader.GetInt32(1),
            Balance: reader.GetInt32(2));
    }

    public void ChangeBankAccountBalance(long accountId, int newBalance)
    {
        const string sql = """
                           update bank_accounts
                           set balance = :new_balance
                           where account_id = :account_id;
                           """;
        using NpgsqlConnection? connection = _connectionProvider
            .GetConnectionAsync(default)
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("new_balance", newBalance)
            .AddParameter("account_id", accountId);

        command.ExecuteNonQuery();
    }

    public long? AddBankAccount(int accountPin)
    {
        const string sql = """
                           insert into bank_accounts(account_pin, balance)
                           values (:account_pin, 0)
                           returning account_id;
                           """;
        using NpgsqlConnection? connection = _connectionProvider
            .GetConnectionAsync(default)
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("account_pin", accountPin);

        using NpgsqlDataReader reader = command.ExecuteReader();
        
        if (reader.Read() is false)
            return null;
        
        return reader.GetInt64(0);
    }
}