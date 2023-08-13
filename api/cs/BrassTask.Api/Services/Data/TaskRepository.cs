namespace BrassTask.Api.Services.Data;

using System.Data;
using BrassTask.Api.Domain;
using BrassTask.Api.Infrastructure.Configuration;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

public class TaskRepository : ITaskRepository
{
    private readonly DatabaseOptions _dbOptions;

    public TaskRepository(IOptions<DatabaseOptions> dbOptions)
    {
        _dbOptions = dbOptions.Value;
    }

    public async Task CreateAsync(ScheduledTask task)
    {
        using var connection = GetOpenConnection();
        var taskId = await connection.ExecuteScalarAsync<Guid>(
            @"INSERT INTO [dbo].[Task] ([UserId], [TaskName], [Description], [ReminderDate], [RepeatInterval], [IsRepeatEnabled])
                OUTPUT INSERTED.[TaskId] VALUES (@UserId, @TaskName, @Description, @ReminderDate, @RepeatInterval, @IsRepeatEnabled)",
            task);

        task.TaskId = taskId;
    }

    public async Task<ScheduledTask?> GetByIdAsync(Guid taskId)
    {
        using var connection = GetOpenConnection();
        return await connection.QuerySingleOrDefaultAsync<ScheduledTask>("SELECT * FROM [dbo].[Task] WHERE [TaskId] = @TaskId", new { TaskId = taskId });
    }

    private IDbConnection GetOpenConnection()
    {
        var connection = new SqlConnection(_dbOptions.ConnectionString);
        connection.Open();
        return connection;
    }
}
