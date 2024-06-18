using PredictionsCup.Models;

namespace PredictionsCup.Services;

public interface IUserService
{
    Task<User> GetUserByIdAsync(int userId);
    Task<List<User>> GetAllUsersAsync();
    Task CreateUserAsync(User newUser);
    Task UpdateUserAsync(User userToUpdate);
    Task DeleteUserAsync(int userId);
}
