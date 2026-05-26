using Lab10.Domain2.Interfaces;
using Lab10.Domain2.Models;
using Lab10.Infrastructure2.Context;
using Microsoft.EntityFrameworkCore;

namespace Lab10.Infrastructure2.Repositories;
public class UserRepository : IUserRepository
{
    private readonly Lab10Context _context;
    public UserRepository(Lab10Context context)
    {
        _context = context;
    }
    public async Task<User?> GetByEmailAsync(string email)
    {
        // Usamos Include para traer el nombre del Rol asignado en SQL
        return await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == email);
    }
}
