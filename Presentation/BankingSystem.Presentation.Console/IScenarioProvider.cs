using System.Diagnostics.CodeAnalysis;

namespace BankingSystem.Presentation.Console;

public interface IScenarioProvider
{
    bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario);
}