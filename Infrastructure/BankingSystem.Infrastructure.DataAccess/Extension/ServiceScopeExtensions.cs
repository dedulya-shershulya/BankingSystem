using Itmo.Dev.Platform.Postgres.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace BankingSystem.Infrastructure.DataAccess.Extension;

public static class ServiceScopeExtensions
{
    public static void UseInfrastructureDataAccess(this IServiceScope scope)
    {
        scope
            .UsePlatformMigrationsAsync(default)
            .GetAwaiter()
            .GetResult();
    }
}