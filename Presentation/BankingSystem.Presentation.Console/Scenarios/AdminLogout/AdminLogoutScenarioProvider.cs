using System.Diagnostics.CodeAnalysis;
using BankingSystem.Application.Contracts.Admin;

namespace BankingSystem.Presentation.Console.Scenarios.AdminLogout;

public class AdminLogoutScenarioProvider : IScenarioProvider
{
    private readonly IAdminService _adminService;
    private readonly ICurrentAdminAccountService _currentAdminAccountService;

    public AdminLogoutScenarioProvider(IAdminService adminService,
        ICurrentAdminAccountService currentAdminAccountService)
    {
        _adminService = adminService;
        _currentAdminAccountService = currentAdminAccountService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (!_currentAdminAccountService.IsVerified)
        {
            scenario = null;
            return false;
        }

        scenario = new AdminLogoutScenario(_adminService);
        return true;
    }
}