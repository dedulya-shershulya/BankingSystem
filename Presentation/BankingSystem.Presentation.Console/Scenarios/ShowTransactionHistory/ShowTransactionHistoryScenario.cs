using BankingSystem.Application.Contracts.BankAccounts;
using BankingSystem.Application.Models.Transactions;
using Spectre.Console;

namespace BankingSystem.Presentation.Console.Scenarios.ShowTransactionHistory;

public class ShowTransactionHistoryScenario : IScenario
{
    private readonly IBankAccountService _service;

    public ShowTransactionHistoryScenario(IBankAccountService service)
    {
        _service = service;
    }

    public string Name => "Show history";

    public void Run()
    {
        ShowHistoryResult result = _service.ShowHistory();

        switch (result)
        {
            case ShowHistoryResult.NotFound:
                AnsiConsole.Markup("[red]Bank account not found[/]");
                break;
            case ShowHistoryResult.EmptyHistory:
                AnsiConsole.Markup("[yellow]The history is empty[/]");
                break;
            case ShowHistoryResult.Success success:
            {
                var table = new Table();
                table.Border(TableBorder.Rounded);
                table.Title("[underline blue]Transaction History[/]");
                
                table.AddColumn(new TableColumn("[bold]Type[/]").Centered());
                table.AddColumn(new TableColumn("[bold]Amount[/]").RightAligned());
                
                foreach (Operation transaction in success.Transactions)
                {
                    string typeDisplay = transaction.Type switch
                    {
                        OperationType.Deposit => "[green]↑ Deposit[/]",
                        OperationType.Withdraw => "[red]↓ Withdraw[/]",
                        _ => "[grey]? Unknown[/]"
                    };
                    
                    string amountDisplay = transaction.Type switch
                    {
                        OperationType.Deposit => $"[green]+{transaction.Amount}[/]",
                        OperationType.Withdraw => $"[red]-{transaction.Amount}[/]",
                        _ => $"[grey]{transaction.Amount}[/]"
                    };
                    
                    table.AddRow(typeDisplay, amountDisplay);
                }
                AnsiConsole.Write(table);
                break;
            }
        }

        AnsiConsole.Markup("\n[yellow]Press any key to continue...[/]");
        AnsiConsole.Console.Input.ReadKey(true);
        AnsiConsole.WriteLine();
    }
}