using Dapper;
using Microsoft.AspNetCore.Connections;
using Quala.Backend.Data;
using Quala.Backend.Models;
using Quala.Backend.Repositories.Interfaces;
using System.Data;
using static Quala.Backend.Constants.AppConstants;

namespace Quala.Backend.Repositories;

    public class UserRepository : IUserRepository
    {
        private readonly QualaDbContext _context;

        public UserRepository(QualaDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            using var connection = _context.CreateConnection();

             return await connection.QueryFirstOrDefaultAsync<User>(
                StoredProcedures.UserGetByUsername,
                new { Username = username },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            var user = await GetUserByUsernameAsync(username);

            if (user == null)
                return false;

            return password == user.PasswordHash;
        }

        public async Task<int> CreateUserAsync(User user)
        {
            using var connection = _context.CreateConnection();

            user.IsActive = true;
            user.CreatedAt = DateTime.UtcNow;

            var userId = await connection.ExecuteScalarAsync<int>(
               StoredProcedures.UserCreate,
                new
                {
                    user.Username,
                    user.PasswordHash,
                    user.Email,
                    user.IsActive,
                    user.CreatedAt
                },
                commandType: CommandType.StoredProcedure
            );

            return userId;
        }
    }
