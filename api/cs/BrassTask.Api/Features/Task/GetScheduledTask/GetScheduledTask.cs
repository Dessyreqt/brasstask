namespace BrassTask.Api.Features.Task.GetScheduledTask;

using BrassTask.Api.Domain;
using BrassTask.Api.Services.Data;
using FluentValidation;
using MediatR;

public class Request : IRequest<ScheduledTask?>
{
    public Guid TaskId { get; set; }
    public Guid UserId { get; set; }
}

public class Validation : AbstractValidator<Request>
{
    public Validation(IUserRepository userRepo)
    {
        RuleFor(x => x.TaskId).NotEmpty();
    }
}

public class Handler : IRequestHandler<Request, ScheduledTask?>
{
    private readonly ITaskRepository _taskRepository;

    public Handler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<ScheduledTask?> Handle(Request request, CancellationToken cancellationToken)
    {
        var scheduledTask = await _taskRepository.GetByIdAsync(request.TaskId);

        if (scheduledTask?.UserId != request.UserId)
        {
            return null;
        }

        return scheduledTask;
    }
}
