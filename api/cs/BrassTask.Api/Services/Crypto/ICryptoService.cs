namespace BrassTask.Api.Services.Crypto;

public interface ICryptoService
{
    Task<string> ComputeArgon2idHash(string input, string salt);
    string GeneratePasswordSalt(int length, string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-_");
}
