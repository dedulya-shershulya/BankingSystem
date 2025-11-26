using BankingSystem.Application.Models.Transactions;

namespace BankingSystem.Application.Contracts.BankAccounts;

public abstract record ShowHistoryResult()
{
    public sealed record Success(IEnumerable<Operation> Transactions) : ShowHistoryResult;

    public sealed record NotFound : ShowHistoryResult;

    public sealed record EmptyHistory : ShowHistoryResult;
}