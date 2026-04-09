using FCG.Domain.Dtos.Models.Identity;
using FluentValidation;

namespace FCG.Domain.Dtos.Validators.Identity;

public sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}
