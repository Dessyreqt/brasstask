namespace BrassTask.Api.Tests.Features.User.Actions.CreateUser;

using BrassTask.Api.Features.User.Actions.CreateUser;
using BrassTask.Api.Identity;
using FluentAssertions;
using NSubstitute;

[Collection(nameof(TestFixture))]
public class CreateUserTests
{
    private readonly TestFixture _fixture;

    public CreateUserTests(TestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task ShouldCreateUser()
    {
        // Assemble
        var request = new Request
        {
            Username = TestConstants.ValidUsername,
            Email = TestConstants.ValidEmail,
            Password = TestConstants.ValidPassword,
            ConfirmPassword = TestConstants.ValidPassword,
        };

        // Act
        var response = await _fixture.SendAsync(request);

        // Assert
        await TestDependencies.UserRepository.Received(1).CreateUserAsync(Arg.Any<User>(), Arg.Any<string>());
        response.UserId.Should().Be(TestConstants.CreatedUserId);
    }
}
