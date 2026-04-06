using FluentValidation;
using FCG.Application.Dtos.Auth;
using FCG.Application.Validation;
using FCG.Domain.Entities;

namespace FCG.Application.Validators;

public sealed class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(User.MaxNameLength);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).StrongPassword();
    }
}
