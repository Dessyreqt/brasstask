namespace BrassTask.Api.Identity;

public class User
{
    public Guid UserId { get; set; } = Guid.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordSalt { get; set; } = string.Empty;
}
