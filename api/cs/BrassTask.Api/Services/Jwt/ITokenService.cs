namespace BrassTask.Api.Services.Jwt;

public interface ITokenService
{
    string GenerateToken(string userId, string username);
}
