using BankingSystem.Application.Extensions;
using BankingSystem.Infrastructure.DataAccess.Extension;
using BankingSystem.Presentation.Console;
using BankingSystem.Presentation.Console.Extensions;
using Microsoft.Extensions.DependencyInjection;

var collection = new ServiceCollection();

collection
    .AddApplication()
    .AddInfrastructureDataAccess(configuration =>
    {
        configuration.Host = "localhost";
        configuration.Port = 6432;
        configuration.Username = "postgres";
        configuration.Password = "postgres";
        configuration.Database = "postgres";
        configuration.SslMode = "Prefer";
    })
    .AddPresentationConsole();

ServiceProvider provider = collection.BuildServiceProvider();
using IServiceScope scope = provider.CreateScope();

scope.UseInfrastructureDataAccess();

ScenarioRunner scenarioRunner = scope.ServiceProvider
    .GetRequiredService<ScenarioRunner>();

while (true)
{
    Console.Clear();
    scenarioRunner.Run();
}