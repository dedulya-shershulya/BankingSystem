using BankingSystem.Application.Contracts.BankAccounts;
using Spectre.Console;

namespace BankingSystem.Presentation.Console.Scenarios.UserLogin;

public class UserLoginScenario : IScenario
{
    private readonly IBankAccountService _bankAccountService;

    public UserLoginScenario(IBankAccountService bankAccountService)
    {
        _bankAccountService = bankAccountService;
    }

    public string Name => "Login as User";

    public void Run()
    {
        long accountId = AnsiConsole.Ask<long>("Enter your account ID: ");
        int accountPin = AnsiConsole.Ask<int>("Enter your account PIN: ");

        LoginResult result = _bankAccountService.Login(accountId, accountPin);

        string message = result switch
        {
            LoginResult.Success => "Login successful!",
            LoginResult.NotFound => "No such bank account. Please, try another ID.",
            LoginResult.WrongPassword => "Wrong PIN.",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Markup("[yellow]Press any key to continue...[/]");
        AnsiConsole.Console.Input.ReadKey(true);
    }
}