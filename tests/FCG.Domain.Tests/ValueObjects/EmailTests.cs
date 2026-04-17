using FCG.Domain.Shared;
using FCG.Domain.Entities.Identity;

namespace FCG.Domain.Tests.ValueObjects;

public sealed class EmailTests
{
    [Theory]
    [InlineData("user@example.com")]
    [InlineData("a@b.co")]
    [InlineData("name+tag@domain.org")]
    public void Create_WithValidFormat_ReturnsNormalizedEmail(string input)
    {
        var email = Email.Create(input);

        Assert.Equal(input.Trim().ToLowerInvariant(), email.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WhenMissing_Throws(string input)
    {
        Assert.Throws<DomainValidationException>(() => Email.Create(input));
    }

    [Fact]
    public void Create_WhenNull_Throws()
    {
        Assert.Throws<DomainValidationException>(() => Email.Create(null!));
    }

    [Theory]
    [InlineData("not-an-email")]
    [InlineData("@nodomain.com")]
    [InlineData("spaces in@mail.com")]
    public void Create_WhenInvalidFormat_Throws(string input)
    {
        Assert.Throws<DomainValidationException>(() => Email.Create(input));
    }
}
