using BankingSystem.Application.Contracts.BankAccounts;

namespace BankingSystem.Application.Contracts.Admin;

public interface IAdminService
{
    LoginResult Login(string password);

    void ChangePassword(string newPassword);

    CreateBankAccountResult CreateBankAccount(int accountPin);

    void Logout();
}