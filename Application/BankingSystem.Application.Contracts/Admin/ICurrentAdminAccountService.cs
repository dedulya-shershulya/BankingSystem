namespace BankingSystem.Application.Contracts.Admin;

public interface ICurrentAdminAccountService
{
    bool IsVerified { get; set; }
}