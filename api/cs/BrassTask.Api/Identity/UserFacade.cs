namespace BrassTask.Api.Identity;

using BrassTask.Api.Infrastructure.Configuration;
using BrassTask.Api.Services.Crypto;
using BrassTask.Api.Services.Data;
using BrassTask.Api.Services.Jwt;
using Microsoft.Extensions.Options;

public class UserFacade
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly ICryptoService _cryptoService;
    private readonly UserOptions _userOptions;

    public UserFacade(IUserRepository userRepository, ITokenService tokenService, ICryptoService cryptoService, IOptions<UserOptions> userOptions)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _cryptoService = cryptoService;
        _userOptions = userOptions.Value;
    }

    public async Task<string?> AuthenticateAsync(string username, string password)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        if (user == null)
        {
            return null; // Authentication failed
        }

        var passwordHash = await _cryptoService.ComputeArgon2idHash(password, user.PasswordSalt);
        var storedHash = await _userRepository.GetPasswordHashByUsernameAsync(username);

        if (passwordHash != storedHash)
        {
            return null; // Authentication failed
        }

        var token = _tokenService.GenerateToken(user.UserId.ToString(), user.Username);
        return token;
    }

    public async Task<User?> CreateAsync(string username, string email, string password)
    {
        var passwordSalt = _cryptoService.GeneratePasswordSalt(_userOptions.PasswordSaltLength);
        var passwordHash = await _cryptoService.ComputeArgon2idHash(password, passwordSalt);
        var user = new User
        {
            Username = username,
            Email = email,
            PasswordSalt = passwordSalt
        };

        await _userRepository.CreateUserAsync(user, passwordHash);

        if (user.UserId != Guid.Empty) { return user; }

        return null;
    }
}
