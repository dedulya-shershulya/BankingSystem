using BankingSystem.Application.Contracts.Admin;
using Spectre.Console;

namespace BankingSystem.Presentation.Console.Scenarios.ChangeSystemPassword;

public class ChangeSystemPasswordScenario : IScenario
{
    private readonly IAdminService _service;

    public ChangeSystemPasswordScenario(IAdminService service)
    {
        _service = service;
    }

    public string Name => "Change System Password";

    public void Run()
    {
        string newPassword = AnsiConsole.Ask<string>("Enter new password: ");
        _service.ChangePassword(newPassword);

        AnsiConsole.WriteLine("Successfully changed password");
        AnsiConsole.Markup("[yellow]Press any key to continue...[/]");
        AnsiConsole.Console.Input.ReadKey(true);
    }
}