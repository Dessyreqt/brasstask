namespace BrassTask.Api.Features.Task;

using System.Security.Claims;
using BrassTask.Api.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize]
[Route("api/task")]
public class TaskController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a task from the supplied request.
    /// </summary>
    /// <param name="request">The task to be created.</param>
    /// <returns>The id of the created task.</returns>
    [HttpPost]
    public async Task<ActionResult<CreateScheduledTask.Response>> CreateScheduledTask(CreateScheduledTask.Request request)
    {
        var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            return Unauthorized();
        }

        if (request.UserId != Guid.Parse(userIdClaim.Value))
        {
            return Unauthorized();
        }

        var response = await _mediator.Send(request);

        if (response.TaskId == Guid.Empty)
        {
            return StatusCode(500);
        }

        var actionName = nameof(GetScheduledTask);
        var controllerName = "Task";
        var routeValues = new { taskId = response.TaskId };

        return CreatedAtAction(actionName, controllerName, routeValues, response);
    }

    /// <summary>
    /// Gets the task with the specified <paramref name="taskId"/>.
    /// </summary>
    /// <param name="taskId">The id of the task to return.</param>
    /// <returns>The task with the specified <paramref name="taskId"/>, if it exists.</returns>
    [HttpGet("{taskId}")]
    public async Task<ActionResult<ScheduledTask>> GetScheduledTask(Guid taskId)
    {
        var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            return Unauthorized();
        }

        var request = new GetScheduledTask.Request { TaskId = taskId, UserId = Guid.Parse(userIdClaim.Value) };
        var response = await _mediator.Send(request);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }
}
