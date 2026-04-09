using FCG.Domain.Dtos.Models.Catalog;
using FCG.Infrastructure.Entities;
using FluentValidation;

namespace FCG.Application.Validators;

public sealed class CreateGameRequestValidator : AbstractValidator<CreateGameRequest>
{
    public CreateGameRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(Game.MaxTitleLength);
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
    }
}
