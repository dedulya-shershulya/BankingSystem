using Spectre.Console;

namespace BankingSystem.Presentation.Console;

public class ScenarioRunner
{
    private readonly IEnumerable<IScenarioProvider> _providers;

    public ScenarioRunner(IEnumerable<IScenarioProvider> providers)
    {
        _providers = providers;
    }

    public void Run()
    {
        IEnumerable<IScenario> scenarios = GetScenarios();

        AnsiConsole.Write(new Rule("[blue]Banking Menu[/]").RuleStyle("blue").LeftJustified());
        AnsiConsole.WriteLine();
        
        SelectionPrompt<IScenario> selector = new SelectionPrompt<IScenario>()
            .AddChoices(scenarios)
            .UseConverter(x => $"- {x.Name}");

        IScenario scenario = AnsiConsole.Prompt(selector);
        scenario.Run();
    }

    private IEnumerable<IScenario> GetScenarios()
    {
        foreach (IScenarioProvider provider in _providers)
        {
            if (provider.TryGetScenario(out IScenario? scenario))
                yield return scenario;
        }
    }
}