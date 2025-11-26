namespace BankingSystem.Presentation.Console;

public interface IScenario
{
    string Name { get; }

    void Run();
}