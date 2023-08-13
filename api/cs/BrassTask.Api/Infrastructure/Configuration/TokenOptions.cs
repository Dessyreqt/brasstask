namespace BrassTask.Api.Infrastructure.Configuration;

public class TokenOptions
{
    public static string ConfigSection => "Jwt";

    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public int ExpirationMinutes { get; set; }
}
