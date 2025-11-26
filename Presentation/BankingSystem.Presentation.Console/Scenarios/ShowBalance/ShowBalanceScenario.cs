using BankingSystem.Application.Contracts.BankAccounts;
using Spectre.Console;

namespace BankingSystem.Presentation.Console.Scenarios.ShowBalance;

public class ShowBalanceScenario : IScenario
{
    private readonly IBankAccountService _service;

    public ShowBalanceScenario(IBankAccountService service)
    {
        _service = service;
    }

    public string Name => "Show Balance";

    public void Run()
    {
        ShowBalanceResult result = _service.ShowBalance();

        switch (result)
        {
            case ShowBalanceResult.Success success:
                AnsiConsole.MarkupLine($"[blue]Current Balance:[/] [bold green]{success.Amount}[/]");
                break;
        
            case ShowBalanceResult.NotFound:
                AnsiConsole.MarkupLine("[red]Bank Account not found[/]");
                break;
        
            default:
                throw new ArgumentOutOfRangeException(nameof(result));
        }

        AnsiConsole.Markup("\n[yellow]Press any key to continue...[/]");
        AnsiConsole.Console.Input.ReadKey(true);
        AnsiConsole.WriteLine();
    }
}