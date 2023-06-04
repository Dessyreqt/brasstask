namespace BrassTask.Api.Identity;

using System.Security.Cryptography;
using System.Text;
using BrassTask.Api.Infrastructure.Configuration;
using BrassTask.Api.Services.Data;
using BrassTask.Api.Services.Jwt;
using Konscious.Security.Cryptography;
using Microsoft.Extensions.Options;

public class UserManager
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly UserOptions _userOptions;

    public UserManager(IUserRepository userRepository, ITokenService tokenService, IOptions<UserOptions> userOptions)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _userOptions = userOptions.Value;
    }

    public async Task<string?> AuthenticateAsync(string username, string password)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        if (user == null)
        {
            return null; // Authentication failed
        }

        var passwordHash = await ComputeArgon2idHash(password, user.PasswordSalt);
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
        var passwordSalt = GeneratePasswordSalt(_userOptions.PasswordSaltLength);
        var passwordHash = await ComputeArgon2idHash(password, passwordSalt);
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

    private static async Task<string> ComputeArgon2idHash(string input, string salt)
    {
        var argon2id = new Argon2id(Encoding.UTF8.GetBytes(input))
        {
            Salt = Encoding.UTF8.GetBytes(salt),
            DegreeOfParallelism = 1,
            Iterations = 3,
            MemorySize = 12288,
        };

        var hash = await argon2id.GetBytesAsync(16);

        return Convert.ToBase64String(hash);
    }

    private static string GeneratePasswordSalt(int length, string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-_")
    {
        using var rng = RandomNumberGenerator.Create();
        var data = new byte[length];

        // If chars.Length isn't a power of 2 then there is a bias if we simply use the modulus operator. The first characters of chars will be more probable than the last ones.
        // buffer used if we encounter an unusable random byte. We will regenerate it in this buffer
        byte[]? buffer = null;

        // Maximum random number that can be used without introducing a bias
        var maxRandom = byte.MaxValue - (byte.MaxValue + 1) % chars.Length;

        rng.GetBytes(data);

        var result = new char[length];

        for (var i = 0; i < length; i++)
        {
            var value = data[i];

            while (value > maxRandom)
            {
                buffer ??= new byte[1];
                rng.GetBytes(buffer);
                value = buffer[0];
            }

            result[i] = chars[value % chars.Length];
        }

        return new string(result);
    }
}
