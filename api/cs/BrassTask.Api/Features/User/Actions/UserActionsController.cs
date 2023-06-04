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
    /// Allows a user to register. The user then receives an email asking them to confirm their email, which contains a token that is sent back to the "confirmEmail" endpoint.
    /// </summary>
    [HttpPost("register")]
    public async Task<ActionResult<Register.Response>> ActionRegister([FromBody] Register.Request request)
    {
        var response = await _mediator.Send(request);

        if (response.UserId != Guid.Empty)
        {
            var actionName = nameof(UserController.GetUser);
            var controllerName = "User";
            var routeValues = new
            {
                userId = response.UserId
            };
            return CreatedAtAction(actionName, controllerName, routeValues, response);
        }

        return StatusCode(500);
    }
}
