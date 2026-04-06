using FluentValidation;
using FCG.Application.Dtos.Promotions;

namespace FCG.Application.Validators;

public sealed class CreatePromotionRequestValidator : AbstractValidator<CreatePromotionRequest>
{
    public CreatePromotionRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.DiscountPercent).InclusiveBetween(0, 100);
        RuleFor(x => x.ValidUntil).GreaterThan(x => x.ValidFrom)
            .WithMessage("ValidUntil must be after ValidFrom.");
    }
}
