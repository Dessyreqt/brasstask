namespace BrassTask.Api.Features.User.Actions.CreateUser;

using BrassTask.Api.Identity;
using BrassTask.Api.Services.Data;
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
    private readonly IUserRepository _userRepo;

    public Validation(IUserRepository userRepo)
    {
        _userRepo = userRepo;

        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Username).MustAsync(HaveUniqueUsername).WithMessage("Username is taken.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Email).MustAsync(HaveUniqueEmail).WithMessage("Email has account registered.");
        RuleFor(x => x.Password).Length(6, 100);
        RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("'Confirm Password' should match 'Password'.");
    }

    private async Task<bool> HaveUniqueUsername(string username, CancellationToken cancellationToken)
    {
        if (await _userRepo.GetUserByUsernameAsync(username) is not null) { return false; }

        return true;
    }

    private async Task<bool> HaveUniqueEmail(string email, CancellationToken cancellationToken)
    {
        if (await _userRepo.GetUserByEmailAsync(email) is not null) { return false; }

        return true;
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
