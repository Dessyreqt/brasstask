namespace BrassTask.Api.Services.Data;

using BrassTask.Api.Identity;

public interface IUserRepository
{
    Task CreateUserAsync(User user, string passwordHash);
    Task<string> GetPasswordHashByUsernameAsync(string username);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User?> GetUserByEmailAsync(string email);
}
