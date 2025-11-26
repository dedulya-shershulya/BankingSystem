using System.Diagnostics.CodeAnalysis;
using BankingSystem.Application.Contracts.BankAccounts;

namespace BankingSystem.Presentation.Console.Scenarios.UserLogout;

public class UserLogoutScenarioProvider : IScenarioProvider
{
    private readonly IBankAccountService _bankAccountService;
    private readonly ICurrentBankAccountService _currentBankAccountService;

    public UserLogoutScenarioProvider(
        IBankAccountService bankAccountService,
        ICurrentBankAccountService currentBankAccountService)
    {
        _bankAccountService = bankAccountService;
        _currentBankAccountService = currentBankAccountService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentBankAccountService.BankAccount is null)
        {
            scenario = null;
            return false;
        }

        scenario = new UserLogoutScenario(_bankAccountService);
        return true;
    }
}