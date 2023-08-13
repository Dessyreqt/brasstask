namespace BrassTask.Api.Infrastructure.Configuration;

public class UserOptions
{
    public static string ConfigSection => "User";

    public int PasswordSaltLength { get; set; }
}
