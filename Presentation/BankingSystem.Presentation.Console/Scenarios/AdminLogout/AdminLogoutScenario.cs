using BankingSystem.Application.Contracts.Admin;

namespace BankingSystem.Presentation.Console.Scenarios.AdminLogout;

public class AdminLogoutScenario : IScenario
{
    private readonly IAdminService _adminService;

    public AdminLogoutScenario(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public string Name { get; } = "Logout";
    
    public void Run()
    {
        _adminService.Logout();
    }
}