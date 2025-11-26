using BankingSystem.Application.Contracts.Admin;
using Spectre.Console;

namespace BankingSystem.Presentation.Console.Scenarios.CreateAccount;

public class CreateAccountScenario : IScenario
{
    private readonly IAdminService _service;

    public CreateAccountScenario(IAdminService service)
    {
        _service = service;
    }

    public string Name => "Create account";

    public void Run()
    {
        int accountPin = AnsiConsole.Ask<int>("[blue]Enter account pin:[/] ");

        CreateBankAccountResult result = _service.CreateBankAccount(accountPin);

        switch (result)
        {
            case CreateBankAccountResult.Failed:
                AnsiConsole.MarkupLine("[red]Failed to create account[/]");
                break;
            case CreateBankAccountResult.Success success:
                AnsiConsole.MarkupLine("[green]Successfully created account[/]");
                AnsiConsole.MarkupLine($"[blue]Your account id is[/] {success.AccountId}");
                break;
        }
        
        AnsiConsole.Markup("[yellow]Press any key to continue...[/]");
        AnsiConsole.Console.Input.ReadKey(true);
    }
}