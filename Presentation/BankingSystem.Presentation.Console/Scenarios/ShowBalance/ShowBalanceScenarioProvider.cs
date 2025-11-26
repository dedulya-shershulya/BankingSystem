using System.Diagnostics.CodeAnalysis;
using BankingSystem.Application.Contracts.BankAccounts;

namespace BankingSystem.Presentation.Console.Scenarios.ShowBalance;

public class ShowBalanceScenarioProvider : IScenarioProvider
{
    private readonly IBankAccountService _service;
    private readonly ICurrentBankAccountService _currentBankAccount;

    public ShowBalanceScenarioProvider(
        IBankAccountService bankAccountService,
        ICurrentBankAccountService currentBankAccountService)
    {
        _service = bankAccountService;
        _currentBankAccount = currentBankAccountService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentBankAccount.BankAccount is null)
        {
            scenario = null;
            return false;
        }

        scenario = new ShowBalanceScenario(_service);
        return true;
    }
}