using FCG.Application.Validators.Identity;
using FCG.Domain.Dtos.Models.Identity;
using FCG.Domain.Dtos.Validators.Identity;

namespace FCG.Application.Tests.Validators;

public sealed class RegisterRequestValidatorTests
{
    private readonly RegisterRequestValidator _validator = new();

    [Fact]
    public async Task WhenEmailInvalid_ShouldFail()
    {
        var request = new RegisterRequest
        {
            Name = "Test",
            Email = "not-email",
            Password = "Str0ng!pass"
        };

        var result = await _validator.ValidateAsync(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(RegisterRequest.Email));
    }

    [Fact]
    public async Task WhenPasswordWeak_ShouldFail()
    {
        var request = new RegisterRequest
        {
            Name = "Test",
            Email = "a@b.co",
            Password = "weak"
        };

        var result = await _validator.ValidateAsync(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(RegisterRequest.Password));
    }

    [Fact]
    public async Task WhenValid_ShouldPass()
    {
        var request = new RegisterRequest
        {
            Name = "Test User",
            Email = "user@example.com",
            Password = "Str0ng!pass"
        };

        var result = await _validator.ValidateAsync(request);

        Assert.True(result.IsValid);
    }
}
