using BankingSystem.Application.Contracts.BankAccounts;

namespace BankingSystem.Presentation.Console.Scenarios.UserLogout;

public class UserLogoutScenario : IScenario
{
    private readonly IBankAccountService _bankAccountService;

    public UserLogoutScenario(IBankAccountService bankAccountService)
    {
        _bankAccountService = bankAccountService;
    }

    public string Name { get; } = "Logout";
    
    public void Run()
    {
        _bankAccountService.Logout();
    }
}