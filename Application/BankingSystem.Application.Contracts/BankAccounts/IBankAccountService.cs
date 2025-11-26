using BankingSystem.Application.Contracts;

namespace BankingSystem.Application.Contracts.BankAccounts;

public interface IBankAccountService
{
    LoginResult Login(long accountId, int pin);

    ShowBalanceResult ShowBalance();

    DepositResult Deposit(int amount);

    WithdrawResult Withdraw(int amount);

    ShowHistoryResult ShowHistory();

    void Logout();
}