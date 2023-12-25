namespace BrassTask.Api.Features.Task.GetAllScheduledTasks;

using BrassTask.Api.Domain;
using BrassTask.Api.Services.Data;
using MediatR;

public class Request : IRequest<ScheduledTask?>
{
    public Guid UserId { get; set; }
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
        var scheduledTask = await _taskRepository.GetByUserIdAsync(request.UserId);

        return scheduledTask;
    }
}
