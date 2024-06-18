using Microsoft.EntityFrameworkCore;
using PredictionsCup.Data;
using PredictionsCup.Models;

namespace PredictionsCup.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserByIdAsync(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task CreateUserAsync(User newUser)
    {
        if (newUser == null)
        {
            throw new ArgumentNullException(nameof(newUser));
        }

        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == newUser.Username);
        if (existingUser != null)
        {
            throw new ArgumentException("Username already exists");
        }

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User userToUpdate)
    {
        _context.Entry(userToUpdate).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
