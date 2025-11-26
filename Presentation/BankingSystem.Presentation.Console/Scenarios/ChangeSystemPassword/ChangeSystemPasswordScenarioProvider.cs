using System.Diagnostics.CodeAnalysis;
using BankingSystem.Application.Contracts.Admin;

namespace BankingSystem.Presentation.Console.Scenarios.ChangeSystemPassword;

public class ChangeSystemPasswordScenarioProvider : IScenarioProvider
{
    private readonly IAdminService _service;
    private readonly ICurrentAdminAccountService _currentAdminAccount;

    public ChangeSystemPasswordScenarioProvider(
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

        scenario = new ChangeSystemPasswordScenario(_service);
        return true;
    }
}