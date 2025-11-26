using BankingSystem.Application.Contracts.Admin;

namespace BankingSystem.Application.Admin;

public class CurrentAdminAccountManager : ICurrentAdminAccountService
{
    public bool IsVerified { get; set; } = false;
}