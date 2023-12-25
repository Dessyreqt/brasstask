namespace BrassTask.Api.Tests;

using BrassTask.Api.Identity;
using BrassTask.Api.Services.Crypto;
using BrassTask.Api.Services.Data;
using BrassTask.Api.Services.Jwt;
using NSubstitute;

public class TestDependencies
{
    public static ICryptoService CryptoService { get; private set; } = Substitute.For<ICryptoService>();
    public static ITaskRepository TaskRepository { get; private set; } = Substitute.For<ITaskRepository>();
    public static ITokenService TokenService { get; private set; } = Substitute.For<ITokenService>();
    public static IUserRepository UserRepository { get; private set; } = Substitute.For<IUserRepository>();

    public static void Initialize()
    {
        CryptoService = Substitute.For<ICryptoService>();
        CryptoService.ComputeArgon2idHash(Arg.Any<string>(), Arg.Any<string>()).Returns(Task.FromResult(TestConstants.SaltedHash));

        TaskRepository = Substitute.For<ITaskRepository>();

        TokenService = Substitute.For<ITokenService>();

        UserRepository = Substitute.For<IUserRepository>();
        UserRepository.CreateUserAsync(Arg.Any<User>(), Arg.Any<string>()).Returns(Task.CompletedTask).AndDoes(x => x.Arg<User>().UserId = TestConstants.CreatedUserId);
    }
}
