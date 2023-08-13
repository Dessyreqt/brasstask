namespace BrassTask.Api.Features.Task.CreateScheduledTask;

using BrassTask.Api.Domain;
using BrassTask.Api.Services.Data;
using FluentValidation;
using MediatR;

public class Request : IRequest<Response>
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? ReminderDate { get; set; }
    public int? RepeatInterval { get; set; }
    public bool IsRepeatEnabled { get; set; }
}

public class Response
{
    public Guid TaskId { get; set; }
}

public class Validation : AbstractValidator<Request>
{
    private readonly IUserRepository _userRepo;

    public Validation(IUserRepository userRepo)
    {
        _userRepo = userRepo;

        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}

public class Handler : IRequestHandler<Request, Response>
{
    private readonly ITaskRepository _taskRepository;

    public Handler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        var scheduledTask = new ScheduledTask
        {
            UserId = request.UserId,
            TaskName = request.Name,
            Description = request.Description,
            ReminderDate = request.ReminderDate,
            RepeatInterval = request.RepeatInterval,
            IsRepeatEnabled = request.IsRepeatEnabled
        };

        await _taskRepository.CreateAsync(scheduledTask);

        return new Response { TaskId = scheduledTask.TaskId };
    }
}
