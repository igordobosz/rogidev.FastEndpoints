using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace rogidev.Rotator.Common.Options;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommonOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<AuthOptions>()
            .Bind(configuration.GetSection(AuthOptions.SectionName))
            .ValidateDataAnnotations();

        return services;
    }
}