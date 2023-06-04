namespace BrassTask.Api.Features.User.Actions;

using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/user/actions")]
public class UserActionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserActionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Allows a user to register.
    /// </summary>
    /// <returns>The userId of the new user.</returns>
    [HttpPost("register")]
    public async Task<ActionResult<CreateUser.Response>> CreateUser([FromBody] CreateUser.Request request)
    {
        var response = await _mediator.Send(request);

        if (response.UserId == Guid.Empty) { return StatusCode(500); }

        var actionName = nameof(UserController.GetUser);
        var controllerName = "User";
        var routeValues = new
        {
            userId = response.UserId
        };

        return CreatedAtAction(actionName, controllerName, routeValues, response);
    }
}
