using rogidev.Rotator.Api.Features.User.Login;
using rogidev.Rotator.Api.Features.User.Register;

namespace rogidev.Rotator.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var authOptions = configuration
            .GetSection(AuthOptions.SectionName)
            .Get<AuthOptions>();

        services.AddJWTBearerAuth(authOptions.JwtKey);
        services.AddAuthorization();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IRegisterService, RegisterService>();
        services.AddScoped<ILoginService, LoginService>();

        return services;
    }
}