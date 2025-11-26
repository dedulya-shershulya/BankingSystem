using BankingSystem.Application.Models.Transactions;

namespace BankingSystem.Application.Abstractions.Repositories;

public interface ITransactionRepository
{
    IEnumerable<Operation> GetTransactions(long accountId);

    void AddTransaction(Operation operation, long accountId);
}