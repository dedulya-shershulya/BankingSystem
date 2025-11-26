using BankingSystem.Application.Abstractions.Repositories;
using BankingSystem.Application.Contracts.BankAccounts;
using BankingSystem.Application.Models.BankAccounts;
using BankingSystem.Application.Models.Transactions;

namespace BankingSystem.Application.BankAccounts;

public class BankAccountService : IBankAccountService
{
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly CurrentBankAccountManager _currentBankAccountManager;

    public BankAccountService(
        IBankAccountRepository bankAccountRepository,
        ITransactionRepository transactionRepository,
        CurrentBankAccountManager currentBankAccountManager)
    {
        _bankAccountRepository = bankAccountRepository;
        _transactionRepository = transactionRepository;
        _currentBankAccountManager = currentBankAccountManager;
    }

    public LoginResult Login(long accountId, int pin)
    {
        BankAccount? bankAccount = _bankAccountRepository.FindBankAccountById(accountId);

        if (bankAccount is null)
        {
            return new LoginResult.NotFound();
        }

        if (bankAccount.Pin != pin)
        {
            return new LoginResult.WrongPassword();
        }

        _currentBankAccountManager.BankAccount = bankAccount;
        return new LoginResult.Success();
    }

    public ShowBalanceResult ShowBalance()
    {
        return _currentBankAccountManager.BankAccount is null
            ? new ShowBalanceResult.NotFound()
            : new ShowBalanceResult.Success(_currentBankAccountManager.BankAccount.Balance);
    }

    public DepositResult Deposit(int amount)
    {
        if ( _currentBankAccountManager.BankAccount is null ||
             amount < 0)
        {
            return new DepositResult.Fail();
        }

        _bankAccountRepository.ChangeBankAccountBalance(
            _currentBankAccountManager.BankAccount.Id,
            _currentBankAccountManager.BankAccount.Balance + amount);

        Login(
            _currentBankAccountManager.BankAccount.Id,
            _currentBankAccountManager.BankAccount.Pin);

        _transactionRepository.AddTransaction(
            new Operation(OperationType.Deposit, amount),
            _currentBankAccountManager.BankAccount.Id);

        return new DepositResult.Success();
    }

    public WithdrawResult Withdraw(int amount)
    {
        if (_currentBankAccountManager.BankAccount is null)
        {
            return new WithdrawResult.NotFound();
        }

        if (amount < 0)
        {
            return new WithdrawResult.Failed();
        }

        if (_currentBankAccountManager.BankAccount.Balance < amount)
        {
            return new WithdrawResult.NotEnoughMoney();
        }

        _bankAccountRepository.ChangeBankAccountBalance(
            _currentBankAccountManager.BankAccount.Id,
            _currentBankAccountManager.BankAccount.Balance - amount);

        Login(
            _currentBankAccountManager.BankAccount.Id,
            _currentBankAccountManager.BankAccount.Pin);

        _transactionRepository.AddTransaction(
            new Operation(OperationType.Withdraw, amount),
            _currentBankAccountManager.BankAccount.Id);

        return new WithdrawResult.Success();
    }

    public ShowHistoryResult ShowHistory()
    {
        if (_currentBankAccountManager.BankAccount is null)
        {
            return new ShowHistoryResult.NotFound();
        }

        IEnumerable<Operation> result = _transactionRepository
            .GetTransactions(_currentBankAccountManager.BankAccount.Id);

        if (!result.Any())
        {
            return new ShowHistoryResult.EmptyHistory();
        }

        return new ShowHistoryResult.Success(result);
    }

    public void Logout()
    {
        _currentBankAccountManager.BankAccount = null;
    }
}