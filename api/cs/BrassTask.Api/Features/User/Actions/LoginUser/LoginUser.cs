namespace BrassTask.Api.Features.User.Actions.LoginUser;

using BrassTask.Api.Identity;
using BrassTask.Api.Services.Data;
using FluentValidation;
using MediatR;

public class Request : IRequest<Response>
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class Response
{
    public string Token { get; set; }
}

public class Validation : AbstractValidator<Request>
{
    private readonly IUserRepository _userRepo;

    public Validation(IUserRepository userRepo)
    {
        _userRepo = userRepo;

        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}

public class Handler : IRequestHandler<Request, Response>
{
    private readonly UserManager _userManager;

    public Handler(UserManager userManager)
    {
        _userManager = userManager;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        var token = await _userManager.AuthenticateAsync(request.Username, request.Password);

        if (token is null)
        {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        return new() { Token = token };
    }
}
