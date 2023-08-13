namespace BrassTask.Api.Infrastructure.Configuration;

public class DatabaseOptions
{
    public static string ConfigSection => "Mssql";

    public string ConnectionString { get; set; } = string.Empty;
}
