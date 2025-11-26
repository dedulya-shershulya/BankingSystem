using BankingSystem.Application.Contracts.BankAccounts;
using Spectre.Console;

namespace BankingSystem.Presentation.Console.Scenarios.DepositTransaction;

public class DepositTransactionScenario : IScenario
{
    private readonly IBankAccountService _service;

    public DepositTransactionScenario(IBankAccountService service)
    {
        _service = service;
    }

    public string Name => "Deposit";

    public void Run()
    {
        int amount = AnsiConsole.Ask<int>("[blue]Enter the amount to deposit:[/] ");
        DepositResult result = _service.Deposit(amount);
    
        switch (result)
        {
            case DepositResult.Success:
                AnsiConsole.MarkupLine($"[green]Deposited: +{amount}[/]");
                AnsiConsole.MarkupLine("[bold green]Transaction completed successfully![/]");
                break;
            case DepositResult.Fail:
                AnsiConsole.MarkupLine("[red]Transaction failed[/]");
                AnsiConsole.MarkupLine("[red]Please try again later[/]");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(result));
        }

        AnsiConsole.Markup("\n[yellow]Press any key to continue...[/]");
        AnsiConsole.Console.Input.ReadKey(true);
    }
}