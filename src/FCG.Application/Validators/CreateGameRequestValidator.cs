using FluentValidation;
using FCG.Application.Dtos.Games;
using FCG.Domain.Entities;

namespace FCG.Application.Validators;

public sealed class CreateGameRequestValidator : AbstractValidator<CreateGameRequest>
{
    public CreateGameRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(Game.MaxTitleLength);
    }
}
