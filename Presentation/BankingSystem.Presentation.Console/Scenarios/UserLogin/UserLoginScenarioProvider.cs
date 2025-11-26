using System.Diagnostics.CodeAnalysis;
using BankingSystem.Application.Contracts.Admin;
using BankingSystem.Application.Contracts.BankAccounts;

namespace BankingSystem.Presentation.Console.Scenarios.UserLogin;

public class UserLoginScenarioProvider : IScenarioProvider
{
    private readonly IBankAccountService _service;
    private readonly ICurrentBankAccountService _currentBankAccount;
    private readonly ICurrentAdminAccountService _currentAdminAccount;

    public UserLoginScenarioProvider(
        IBankAccountService bankAccountService,
        ICurrentBankAccountService currentBankAccountService,
        ICurrentAdminAccountService currentAdminAccount)
    {
        _service = bankAccountService;
        _currentBankAccount = currentBankAccountService;
        _currentAdminAccount = currentAdminAccount;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentBankAccount.BankAccount is not null || 
            _currentAdminAccount.IsVerified)
        {
            scenario = null;
            return false;
        }

        scenario = new UserLoginScenario(_service);
        return true;
    }
}