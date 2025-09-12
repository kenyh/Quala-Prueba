using Quala.Backend.Models;

namespace Quala.Backend.Services.Interfaces;
public interface IAuthService
{
    Task<LoginResponse> AuthenticateAsync(string username, string password);
    Task<bool> ValidateCredentialsAsync(string username, string password);
}
