using BankingSystem.Presentation.Console.Scenarios.AdminLogin;
using BankingSystem.Presentation.Console.Scenarios.AdminLogout;
using BankingSystem.Presentation.Console.Scenarios.ChangeSystemPassword;
using BankingSystem.Presentation.Console.Scenarios.CreateAccount;
using BankingSystem.Presentation.Console.Scenarios.DepositTransaction;
using BankingSystem.Presentation.Console.Scenarios.ShowBalance;
using BankingSystem.Presentation.Console.Scenarios.ShowTransactionHistory;
using BankingSystem.Presentation.Console.Scenarios.UserLogin;
using BankingSystem.Presentation.Console.Scenarios.UserLogout;
using BankingSystem.Presentation.Console.Scenarios.WithdrawTransaction;
using Microsoft.Extensions.DependencyInjection;

namespace BankingSystem.Presentation.Console.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationConsole(this IServiceCollection collection)
    {
        collection.AddScoped<ScenarioRunner>();

        collection.AddScoped<IScenarioProvider, UserLoginScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ShowBalanceScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ShowTransactionHistoryScenarioProvider>();
        collection.AddScoped<IScenarioProvider, DepositTransactionScenarioProvider>();
        collection.AddScoped<IScenarioProvider, WithdrawTransactionScenarioProvider>();
        collection.AddScoped<IScenarioProvider, UserLogoutScenarioProvider>();

        collection.AddScoped<IScenarioProvider, AdminLoginScenarioProvider>();
        collection.AddScoped<IScenarioProvider, CreateAccountScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ChangeSystemPasswordScenarioProvider>();
        collection.AddScoped<IScenarioProvider, AdminLogoutScenarioProvider>();

        return collection;
    }
}