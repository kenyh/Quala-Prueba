using Quala.Backend.Models;

namespace Quala.Backend.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User> GetUserByUsernameAsync(string username);
    Task<bool> ValidateCredentialsAsync(string username, string password);
    Task<int> CreateUserAsync(User user);
}
