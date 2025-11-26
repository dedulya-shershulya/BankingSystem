namespace BankingSystem.Application.Contracts.BankAccounts;

public abstract record DepositResult()
{
    public sealed record Success() : DepositResult;

    public sealed record Fail() : DepositResult;
}