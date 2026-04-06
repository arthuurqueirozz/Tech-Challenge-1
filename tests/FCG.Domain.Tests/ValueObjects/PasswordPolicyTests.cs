using FCG.Domain.Exceptions;
using FCG.Domain.ValueObjects;

namespace FCG.Domain.Tests.ValueObjects;

public sealed class PasswordPolicyTests
{
    [Theory]
    [InlineData("Abcd1234!")]
    [InlineData("Str0ng#pass")]
    [InlineData("P@ssw0rd1")]
    public void IsStrong_WithLettersDigitsAndSpecial_ReturnsTrue(string password)
    {
        Assert.True(PasswordPolicy.IsStrong(password));
    }

    [Theory]
    [InlineData("short1!")]
    [InlineData("NoDigitsHere!")]
    [InlineData("NoSpecial123")]
    [InlineData("nonumbers!X")]
    [InlineData("")]
    public void IsStrong_WhenRuleViolated_ReturnsFalse(string password)
    {
        Assert.False(PasswordPolicy.IsStrong(password));
    }

    [Fact]
    public void ValidateOrThrow_WhenStrong_DoesNotThrow()
    {
        var ex = Record.Exception(() => PasswordPolicy.ValidateOrThrow("Valid1a!"));
        Assert.Null(ex);
    }

    [Fact]
    public void ValidateOrThrow_WhenWeak_ThrowsDomainValidationException()
    {
        Assert.Throws<DomainValidationException>(() => PasswordPolicy.ValidateOrThrow("weak"));
    }
}
