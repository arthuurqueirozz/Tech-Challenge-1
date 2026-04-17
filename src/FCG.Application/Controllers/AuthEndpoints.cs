using FCG.Domain.Dtos.Models.Identity;
using FCG.Domain.Interfaces.Identity;
using FluentValidation;

namespace FCG.Application.Controllers;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth").WithTags("Auth");

        group.MapPost("/register", RegisterAsync);
        group.MapPost("/login", LoginAsync);

        return app;
    }

    private static async Task<IResult> RegisterAsync(
        RegisterRequest request,
        IValidator<RegisterRequest> validator,
        IAuthService authService,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        var response = await authService.RegisterAsync(request, cancellationToken);
        return Results.Ok(response);
    }

    private static async Task<IResult> LoginAsync(
        LoginRequest request,
        IValidator<LoginRequest> validator,
        IAuthService authService,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        var response = await authService.LoginAsync(request, cancellationToken);
        return Results.Ok(response);
    }
}
