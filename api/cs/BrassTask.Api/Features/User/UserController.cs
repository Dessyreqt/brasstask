namespace BrassTask.Api.Features.User;

using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets the user with the specified <paramref name="userId"/>.
    /// </summary>
    /// <param name="userId">The id of the user to return.</param>
    /// <returns>The recruiter with the specified <paramref name="userId"/>, if it exists.</returns>
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser(int userId)
    {
        return Ok();
    }
}
