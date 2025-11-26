namespace BankingSystem.Application.Contracts.BankAccounts;

public abstract record WithdrawResult()
{
    public sealed record Success : WithdrawResult;

    public sealed record Failed : WithdrawResult;

    public sealed record NotFound : WithdrawResult;

    public sealed record NotEnoughMoney : WithdrawResult;
}