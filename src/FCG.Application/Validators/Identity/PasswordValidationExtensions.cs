using FCG.Infrastructure.Entities.Identity;
using FluentValidation;

namespace FCG.Application.Validators.Identity;

public static class PasswordValidationExtensions
{
    public static IRuleBuilderOptions<T, string> StrongPassword<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must(PasswordPolicy.IsStrong)
            .WithMessage(
                "Password must be at least 8 characters and contain letters, numbers, and special characters.");
    }
}
