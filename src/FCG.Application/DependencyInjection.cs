using FCG.Application.Services;
using FCG.Application.Validators.Identity;
using FCG.Domain.Dtos.Validators.Identity;
using FCG.Domain.Interfaces;
using FCG.Domain.Interfaces.Identity;
using FCG.Domain.Services;
using FCG.Domain.Services.Identity;
using FluentValidation;

namespace FCG.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserProfileService, UserProfileService>();
        services.AddScoped<IGameCatalogService, GameCatalogService>();
        services.AddScoped<IUserLibraryService, UserLibraryService>();
        services.AddScoped<IAdminUserService, AdminUserService>();
        services.AddScoped<ISaleService, SaleService>();

        return services;
    }
}
