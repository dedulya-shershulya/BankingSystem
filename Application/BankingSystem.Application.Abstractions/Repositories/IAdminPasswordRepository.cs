namespace BankingSystem.Application.Abstractions.Repositories;

public interface IAdminPasswordRepository
{
    string? GetAdminPassword();

    void SetAdminPassword(string password);
}