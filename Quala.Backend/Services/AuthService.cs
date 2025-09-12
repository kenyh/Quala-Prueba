using Microsoft.IdentityModel.Tokens;
using Quala.Backend.Models;
using Quala.Backend.Repositories.Interfaces;
using Quala.Backend.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Quala.Backend.Constants.AppConstants;

namespace Quala.Backend.Services;

public class AuthService : IAuthService
{

    private readonly IConfiguration _configuration;
    private readonly IUserRepository _user;

    public AuthService(IConfiguration configuration, IUserRepository userRepository)
    {
        _user = userRepository;
        _configuration = configuration;
    }

    public async Task<LoginResponse> AuthenticateAsync(string username, string password)
    {
        var isValid = await _user.ValidateCredentialsAsync(username, password);

        if (!isValid)
        {
            return null;
        }

        return GenerateToken(username);
    }

    public LoginResponse GenerateToken(string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration[Jwt.Key]!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _configuration[Jwt.Issuer],
            Audience = _configuration[Jwt.Audience],
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return new LoginResponse
        {
            Token = tokenString,
            Expiration = token.ValidTo,
            Username = username
        };
    }

    public async Task<bool> ValidateCredentialsAsync(string username, string password)
    {
        return await _user.ValidateCredentialsAsync(username, password);
    }
}