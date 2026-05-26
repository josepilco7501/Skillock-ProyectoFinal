namespace Lab10.Application2.Interfaces;

using Lab10.Domain2.Models;

public interface IAuthService
{
    string GenerateJwtToken(User user);
    bool VerifyPassword(User user, string hashedPassword, string providedPassword);
}