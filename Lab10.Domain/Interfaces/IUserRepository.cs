using Lab10.Domain2.Models;

namespace Lab10.Domain2.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
}