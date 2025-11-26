using BankingSystem.Application.Abstractions.Repositories;
using BankingSystem.Application.Contracts.Admin;
using BankingSystem.Application.Contracts.BankAccounts;

namespace BankingSystem.Application.Admin;

public class AdminService : IAdminService
{
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly IAdminPasswordRepository _adminPasswordRepository;
    private readonly ICurrentAdminAccountService _currentAdminAccountService;

    public AdminService(
        IBankAccountRepository bankAccountRepository,
        IAdminPasswordRepository adminPasswordRepository,
        ICurrentAdminAccountService currentAdminAccountService)
    {
        _bankAccountRepository = bankAccountRepository;
        _adminPasswordRepository = adminPasswordRepository;
        _currentAdminAccountService = currentAdminAccountService;
    }

    public LoginResult Login(string password)
    {
        string? systemPassword = _adminPasswordRepository.GetAdminPassword();

        if (systemPassword is null)
        {
            return new LoginResult.NotFound();
        }

        if (systemPassword != password)
        {
            return new LoginResult.WrongPassword();
        }

        _currentAdminAccountService.IsVerified = true;
        return new LoginResult.Success();
    }

    public void ChangePassword(string newPassword)
    {
        _adminPasswordRepository.SetAdminPassword(newPassword);
    }

    public CreateBankAccountResult CreateBankAccount(int accountPin)
    {
        long? accountId = _bankAccountRepository.AddBankAccount(accountPin);
        
        return accountId != null
            ? new CreateBankAccountResult.Success(accountId.Value) 
            : new CreateBankAccountResult.Failed();
    }

    public void Logout()
    {
        _currentAdminAccountService.IsVerified = false;
    }
}