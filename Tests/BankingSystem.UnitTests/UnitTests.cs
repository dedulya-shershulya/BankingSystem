using BankingSystem.Application.Abstractions.Repositories;
using BankingSystem.Application.BankAccounts;
using BankingSystem.Application.Contracts.BankAccounts;
using BankingSystem.Application.Models.BankAccounts;
using NSubstitute;
using Xunit;

namespace BankingSystem.UnitTests;

public class UnitTests
{
    [Fact]
    public void BankAccountService_ShouldReturnSuccess_WhenTopUpTransaction()
    {
        // Arrange
        var manager = new CurrentBankAccountManager();
        var bankAccount = new BankAccount(1, 2, 3);
        manager.BankAccount = bankAccount;
        IBankAccountRepository mockBankAccountRepository = Substitute.For<IBankAccountRepository>();
        mockBankAccountRepository.FindBankAccountById(1).Returns(bankAccount);
        mockBankAccountRepository.ChangeBankAccountBalance(1, 103);
        ITransactionRepository mockTransactionRepository = Substitute.For<ITransactionRepository>();
        var service = new BankAccountService(mockBankAccountRepository, mockTransactionRepository, manager);

        // Act
        service.Login(1, 2);
        DepositResult topUpResult = service.Deposit(100);

        // Assert
        Assert.IsType<DepositResult.Success>(topUpResult);
    }

    [Fact]
    public void BankAccountService_ShouldReturnSuccess_WhenWithdrawTransaction()
    {
        // Arrange
        var manager = new CurrentBankAccountManager();
        var bankAccount = new BankAccount(1, 2, 30);
        manager.BankAccount = bankAccount;
        IBankAccountRepository mockBankAccountRepository = Substitute.For<IBankAccountRepository>();
        mockBankAccountRepository.FindBankAccountById(1).Returns(bankAccount);
        mockBankAccountRepository.ChangeBankAccountBalance(1, 15);
        ITransactionRepository mockTransactionRepository = Substitute.For<ITransactionRepository>();
        var service = new BankAccountService(mockBankAccountRepository, mockTransactionRepository, manager);

        // Act
        service.Login(1, 2);
        WithdrawResult withdrawResult = service.Withdraw(15);

        // Assert
        Assert.IsType<WithdrawResult.Success>(withdrawResult);
    }

    [Fact]
    public void BankAccountService_ShouldReturnNotEnoughMoney_WhenWithdrawTransaction()
    {
        // Arrange
        var manager = new CurrentBankAccountManager();
        var bankAccount = new BankAccount(1, 2, 3);
        manager.BankAccount = bankAccount;
        IBankAccountRepository mockBankAccountRepository = Substitute.For<IBankAccountRepository>();
        mockBankAccountRepository.FindBankAccountById(1).Returns(bankAccount);
        ITransactionRepository mockTransactionRepository = Substitute.For<ITransactionRepository>();
        var service = new BankAccountService(mockBankAccountRepository, mockTransactionRepository, manager);

        // Act
        service.Login(1, 2);
        WithdrawResult withdrawResult = service.Withdraw(100);

        // Assert
        Assert.IsType<WithdrawResult.NotEnoughMoney>(withdrawResult);
    }
}