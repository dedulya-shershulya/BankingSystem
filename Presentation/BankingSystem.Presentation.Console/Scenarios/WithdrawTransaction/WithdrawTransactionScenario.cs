using BankingSystem.Application.Contracts.BankAccounts;
using Spectre.Console;

namespace BankingSystem.Presentation.Console.Scenarios.WithdrawTransaction;

public class WithdrawTransactionScenario : IScenario
{
    private readonly IBankAccountService _service;

    public WithdrawTransactionScenario(IBankAccountService service)
    {
        _service = service;
    }

    public string Name => "Withdraw";

    public void Run()
    {
        int amount = AnsiConsole.Ask<int>("[blue]Enter the amount to withdraw:[/] ");
        WithdrawResult result = _service.Withdraw(amount);

        switch (result)
        {
            case WithdrawResult.Success:
                AnsiConsole.MarkupLine($"[green]Successfully withdrew {amount}![/]");
                break;
            case WithdrawResult.Failed:
                AnsiConsole.MarkupLine("[red]Withdrawal failed[/]");
                break;
            case WithdrawResult.NotFound:
                AnsiConsole.MarkupLine("[red]Account not found[/]");
                break;
            case WithdrawResult.NotEnoughMoney:
                AnsiConsole.MarkupLine("[orange3]Not enough money[/]");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(result));
        }

        AnsiConsole.Markup("\n[yellow]Press any key to continue...[/]");
        AnsiConsole.Console.Input.ReadKey(true);
    }
}