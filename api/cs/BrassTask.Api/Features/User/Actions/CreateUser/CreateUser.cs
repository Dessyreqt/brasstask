namespace BrassTask.Api.Features.User.Actions.CreateUser;

using BrassTask.Api.Identity;
using FluentValidation;
using MediatR;

public class Request : IRequest<Response>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}

public class Response
{
    public Guid UserId { get; set; }
}

public class Validation : AbstractValidator<Request>
{
    public Validation()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).Length(6, 100);
        RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("'Confirm Password' should match 'Password'.");
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
        var user = await _userManager.CreateAsync(request.Username, request.Email, request.Password);

        if (user is not null)
        {
            return new Response
            {
                UserId = user.UserId
            };
        }

        return new Response
        {
            UserId = Guid.Empty
        };
    }
}
