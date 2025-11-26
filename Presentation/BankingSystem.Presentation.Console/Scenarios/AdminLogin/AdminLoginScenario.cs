using BankingSystem.Application.Contracts.Admin;
using BankingSystem.Application.Contracts.BankAccounts;
using Spectre.Console;

namespace BankingSystem.Presentation.Console.Scenarios.AdminLogin;

public class AdminLoginScenario : IScenario
{
    private readonly IAdminService _service;

    public AdminLoginScenario(IAdminService service)
    {
        _service = service;
    }

    public string Name => "Login as Admin";

    public void Run()
    {
        string password = AnsiConsole.Ask<string>("Enter password: ");

        LoginResult result = _service.Login(password);

        string message = result switch
        {
            LoginResult.Success => "Login successfully",
            LoginResult.NotFound => "Something went wrong",
            LoginResult.WrongPassword => "Wrong password",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Markup("[yellow]Press any key to continue...[/]");
        AnsiConsole.Console.Input.ReadKey(true);
    }
}