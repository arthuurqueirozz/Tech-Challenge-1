using FCG.Domain.Dtos.Models.Sales;
using FCG.Domain.Interfaces;
using FluentValidation;

namespace FCG.Application.Controllers;

public static class AdminSalesEndpoints
{
    public static IEndpointRouteBuilder MapAdminSalesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/admin/sales").RequireAuthorization("Admin").WithTags("Sales");

        group.MapGet("/", ListSalesAsync);
        group.MapPost("/", CreateSaleAsync);

        return app;
    }

    private static async Task<IResult> ListSalesAsync(
        ISaleService sales,
        CancellationToken cancellationToken)
    {
        var list = await sales.ListAsync(cancellationToken);
        return Results.Ok(list);
    }

    private static async Task<IResult> CreateSaleAsync(
        CreateSaleRequest request,
        IValidator<CreateSaleRequest> validator,
        ISaleService sales,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        var created = await sales.CreateAsync(request, cancellationToken);
        return Results.Created($"/api/admin/sales/{created.Id}", created);
    }
}
