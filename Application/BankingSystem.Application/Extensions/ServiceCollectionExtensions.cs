using BankingSystem.Application.Admin;
using BankingSystem.Application.BankAccounts;
using BankingSystem.Application.Contracts.Admin;
using BankingSystem.Application.Contracts.BankAccounts;
using Microsoft.Extensions.DependencyInjection;

namespace BankingSystem.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IBankAccountService, BankAccountService>();
        collection.AddScoped<IAdminService, AdminService>();

        collection.AddScoped<CurrentBankAccountManager>();
        collection.AddScoped<ICurrentBankAccountService>(p => p.GetRequiredService<CurrentBankAccountManager>());
        collection.AddScoped<CurrentAdminAccountManager>();
        collection.AddScoped<ICurrentAdminAccountService>(p => p.GetRequiredService<CurrentAdminAccountManager>());

        return collection;
    }
}