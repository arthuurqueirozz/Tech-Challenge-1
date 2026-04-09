using FCG.Domain.Dtos.Models.Sales;
using FluentValidation;

namespace FCG.Domain.Dtos.Validators;

public sealed class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.DiscountPercent).InclusiveBetween(0, 100);
        RuleFor(x => x.ValidUntil).GreaterThan(x => x.ValidFrom)
            .WithMessage("ValidUntil must be after ValidFrom.");
        RuleFor(x => x.GameId).NotEmpty();
    }
}
