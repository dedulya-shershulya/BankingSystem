namespace BankingSystem.Application.Contracts.Admin;

public abstract record CreateBankAccountResult
{
    public sealed record Success(long AccountId) : CreateBankAccountResult;

    public sealed record Failed : CreateBankAccountResult;
}