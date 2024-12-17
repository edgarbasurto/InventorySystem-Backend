namespace InventorySystem.Application.Interfaces;

public interface IAuthService
{
    Task<string> AuthenticateAsync(string username, string password);
}