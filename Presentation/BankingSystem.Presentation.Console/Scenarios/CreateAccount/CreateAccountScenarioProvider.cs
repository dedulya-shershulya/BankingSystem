using System.Diagnostics.CodeAnalysis;
using BankingSystem.Application.Contracts.Admin;

namespace BankingSystem.Presentation.Console.Scenarios.CreateAccount;

public class CreateAccountScenarioProvider : IScenarioProvider
{
    private readonly IAdminService _service;
    private readonly ICurrentAdminAccountService _currentAdminAccount;

    public CreateAccountScenarioProvider(
        IAdminService service,
        ICurrentAdminAccountService currentAdminAccount)
    {
        _service = service;
        _currentAdminAccount = currentAdminAccount;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (!_currentAdminAccount.IsVerified)
        {
            scenario = null;
            return false;
        }

        scenario = new CreateAccountScenario(_service);
        return true;
    }
}