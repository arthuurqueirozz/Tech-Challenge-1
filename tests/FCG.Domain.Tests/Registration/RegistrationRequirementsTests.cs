using FCG.Domain.Shared;
using FCG.Domain.Entities.Identity;

namespace FCG.Domain.Tests.Registration;

/// <summary>
/// BDD-style scenarios for user registration: email + password rules and aggregate creation.
/// </summary>
public sealed class RegistrationRequirementsTests
{
    [Fact]
    public void GivenValidEmailAndPassword_WhenCreatingUser_ThenUserHasNormalizedEmailAndRole()
    {
        var email = Email.Create("User@Example.COM");
        PasswordPolicy.ValidateOrThrow("Secure1a!");

        var user = User.Create("Player One", email, "hash", UserRole.User);

        Assert.Equal("user@example.com", user.Email.Value);
        Assert.Equal(UserRole.User, user.Role);
        Assert.Equal("Player One", user.Name);
    }

    [Fact]
    public void GivenInvalidEmail_WhenCreatingEmail_ThenThrows()
    {
        Assert.Throws<DomainValidationException>(() => Email.Create("bad"));
    }

    [Fact]
    public void GivenWeakPassword_WhenValidatingPolicy_ThenThrows()
    {
        Assert.Throws<DomainValidationException>(() => PasswordPolicy.ValidateOrThrow("abc"));
    }
}
