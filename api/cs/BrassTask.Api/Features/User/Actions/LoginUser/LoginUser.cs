namespace BrassTask.Api.Features.User.Actions.LoginUser;

using BrassTask.Api.Identity;
using BrassTask.Api.Services.Data;
using FluentValidation;
using MediatR;

public class Request : IRequest<Response>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class Response
{
    public string Token { get; set; } = string.Empty;
    public Guid UserId { get; set; }
}

public class Validation : AbstractValidator<Request>
{
    public Validation(IUserRepository userRepo)
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}

public class Handler : IRequestHandler<Request, Response>
{
    private readonly UserManager _userManager;
    private readonly IUserRepository _userRepo;

    public Handler(UserManager userManager, IUserRepository userRepo)
    {
        _userManager = userManager;
        _userRepo = userRepo;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        var token = await _userManager.AuthenticateAsync(request.Username, request.Password);

        if (token is null)
        {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        var user = await _userRepo.GetUserByUsernameAsync(request.Username);

        return new() { Token = token, UserId = user!.UserId };
    }
}
