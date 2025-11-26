using BankingSystem.Application.Contracts.BankAccounts;
using BankingSystem.Application.Models.BankAccounts;

namespace BankingSystem.Application.BankAccounts;

public class CurrentBankAccountManager : ICurrentBankAccountService
{
    public BankAccount? BankAccount { get; set; }
}