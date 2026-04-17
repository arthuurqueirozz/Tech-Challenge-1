using FCG.Application.Validators;
using FCG.Domain.Dtos.Models.Catalog;
using FCG.Domain.Dtos.Validators;

namespace FCG.Application.Tests.Catalog;

public sealed class CreateGameRequestValidatorTests
{
    private readonly CreateGameRequestValidator _validator = new();

    [Fact]
    public async Task WhenPriceIsNegative_ShouldFail()
    {
        var request = new CreateGameRequest
        {
            Title = "Cloud Racer",
            Price = -5m
        };

        var result = await _validator.ValidateAsync(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(CreateGameRequest.Price));
    }
}
