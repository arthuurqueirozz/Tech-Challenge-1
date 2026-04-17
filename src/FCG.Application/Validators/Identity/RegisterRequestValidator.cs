using FCG.Domain.Dtos.Models.Identity;
using FCG.Domain.Entities.Identity;
using FluentValidation;

namespace FCG.Application.Validators.Identity;

public sealed class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(User.MaxNameLength);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).StrongPassword();
    }
}
