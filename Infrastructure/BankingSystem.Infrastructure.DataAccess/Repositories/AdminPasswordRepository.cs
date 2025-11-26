using BankingSystem.Application.Abstractions.Repositories;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace BankingSystem.Infrastructure.DataAccess.Repositories;

public class AdminPasswordRepository : IAdminPasswordRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public AdminPasswordRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public string? GetAdminPassword()
    {
        const string sql = """
                           select *
                           from admin_password
                           """;
        using NpgsqlConnection? connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection);

        using NpgsqlDataReader reader = command.ExecuteReader();

        return reader.Read() is false ? null : reader.GetString(0);
    }

    public void SetAdminPassword(string password)
    {
        const string sql = """
                           update admin_password
                           set password = :password;
                           """;
        using NpgsqlConnection? connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("password", password);

        command.ExecuteNonQuery();
    }
}