using BankingSystem.Application.Models.BankAccounts;

namespace BankingSystem.Application.Contracts.BankAccounts;

public interface ICurrentBankAccountService
{
    BankAccount? BankAccount { get; set; }
}