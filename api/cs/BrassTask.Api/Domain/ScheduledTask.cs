namespace BrassTask.Api.Domain;

public class ScheduledTask
{
    public Guid TaskId { get; set; } = Guid.Empty;
    public Guid UserId { get; set; }
    public string TaskName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? ReminderDate { get; set; }
    public int? RepeatInterval { get; set; }
    public bool IsRepeatEnabled { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
