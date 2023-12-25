namespace BrassTask.Api.Services.Data;

using BrassTask.Api.Domain;

public interface ITaskRepository
{
    Task CreateAsync(ScheduledTask task);
    Task<ScheduledTask?> GetByIdAsync(Guid taskId);
    Task<ScheduledTask?> GetByUserIdAsync(Guid userId);
}
