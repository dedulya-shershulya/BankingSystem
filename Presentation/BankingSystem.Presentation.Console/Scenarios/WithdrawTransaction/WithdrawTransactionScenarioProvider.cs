using System.Diagnostics.CodeAnalysis;
using BankingSystem.Application.Contracts.BankAccounts;

namespace BankingSystem.Presentation.Console.Scenarios.WithdrawTransaction;

public class WithdrawTransactionScenarioProvider : IScenarioProvider
{
    private readonly IBankAccountService _service;
    private readonly ICurrentBankAccountService _currentBankAccount;

    public WithdrawTransactionScenarioProvider(
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

        scenario = new WithdrawTransactionScenario(_service);
        return true;
    }
}