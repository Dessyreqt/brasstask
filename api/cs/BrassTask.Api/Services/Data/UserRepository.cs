namespace BrassTask.Api.Services.Data;

using System.Data;
using BrassTask.Api.Identity;
using BrassTask.Api.Infrastructure.Configuration;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

public class UserRepository : IUserRepository
{
    private readonly DatabaseOptions _dbOptions;

    public UserRepository(IOptions<DatabaseOptions> dbOptions)
    {
        _dbOptions = dbOptions.Value;
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        using var connection = GetOpenConnection();
        return await connection.QuerySingleOrDefaultAsync<User>("SELECT * FROM [User] WHERE [Username] = @Username", new { Username = username });
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        using var connection = GetOpenConnection();
        return await connection.QuerySingleOrDefaultAsync<User>("SELECT * FROM [User] WHERE [Email] = @Email", new { Email = email });
    }

    public async Task<string> GetPasswordHashByUsernameAsync(string username)
    {
        using var connection = GetOpenConnection();
        return await connection.QuerySingleOrDefaultAsync<string>("SELECT [PasswordHash] FROM [User] WHERE [Username] = @Username", new { Username = username });
    }

    public async Task CreateUserAsync(User user, string passwordHash)
    {
        using var connection = GetOpenConnection();
        var userId = await connection.ExecuteScalarAsync<Guid>(
            "INSERT INTO [User] ([Username], [Email], [PasswordSalt], [PasswordHash]) OUTPUT INSERTED.[UserId] VALUES (@Username, @Email, @PasswordSalt, @PasswordHash)",
            new { user.Username, user.Email, user.PasswordSalt, passwordHash });

        user.UserId = userId;
    }

    private IDbConnection GetOpenConnection()
    {
        var connection = new SqlConnection(_dbOptions.ConnectionString);
        connection.Open();
        return connection;
    }
}
