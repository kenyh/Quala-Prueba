
using System.Data;
using System.Data.SqlClient;
using static Quala.Backend.Constants.AppConstants;

namespace Quala.Backend.Data;

public class QualaDbContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public QualaDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString(Database.DefaultConnection);
    }

    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
}