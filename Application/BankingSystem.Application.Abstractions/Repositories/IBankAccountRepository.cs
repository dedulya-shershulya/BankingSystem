using BankingSystem.Application.Models.BankAccounts;

namespace BankingSystem.Application.Abstractions.Repositories;

public interface IBankAccountRepository
{
    BankAccount? FindBankAccountById(long accountId);

    void ChangeBankAccountBalance(long accountId, int newBalance);

    long? AddBankAccount(int accountPin);
}