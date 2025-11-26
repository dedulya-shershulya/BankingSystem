namespace BankingSystem.Application.Contracts.BankAccounts;

public abstract record ShowBalanceResult
{
    public sealed record Success(int Amount) : ShowBalanceResult;

    public sealed record NotFound : ShowBalanceResult;
}