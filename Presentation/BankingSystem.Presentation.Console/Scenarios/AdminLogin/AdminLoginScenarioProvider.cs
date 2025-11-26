using System.Diagnostics.CodeAnalysis;
using BankingSystem.Application.Contracts.Admin;
using BankingSystem.Application.Contracts.BankAccounts;

namespace BankingSystem.Presentation.Console.Scenarios.AdminLogin;

public class AdminLoginScenarioProvider : IScenarioProvider
{
    private readonly IAdminService _service;
    private readonly ICurrentAdminAccountService _currentAdminAccount;
    private readonly ICurrentBankAccountService _currentBankAccountService;

    public AdminLoginScenarioProvider(
        IAdminService service,
        ICurrentAdminAccountService currentAdminAccount,
        ICurrentBankAccountService currentBankAccountService)
    {
        _service = service;
        _currentAdminAccount = currentAdminAccount;
        _currentBankAccountService = currentBankAccountService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAdminAccount.IsVerified ||
            _currentBankAccountService.BankAccount is not null)
        {
            scenario = null;
            return false;
        }

        scenario = new AdminLoginScenario(_service);
        return true;
    }
}