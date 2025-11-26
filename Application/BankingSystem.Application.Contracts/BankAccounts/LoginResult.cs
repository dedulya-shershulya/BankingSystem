namespace BankingSystem.Application.Contracts.BankAccounts;

public abstract record LoginResult()
{
    public sealed record Success : LoginResult;

    public sealed record NotFound : LoginResult;

    public sealed record WrongPassword : LoginResult;
}