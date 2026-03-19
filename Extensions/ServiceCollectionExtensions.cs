using Maib.Checkout.Api.Connector.Models;
using Maib.Checkout.Api.Connector.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Maib.Checkout.Api.Connector.Extensions;

/// <summary>
///     Represents Service Collection extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Adds connector to merchant ecomm service.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="serviceLifetime">The <see cref="T:Microsoft.Extensions.DependencyInjection.ServiceLifetime" /> of the service.</param>
    /// <param name="sectionName">Name of the section.</param>
    public static IServiceCollection AddMaibCheckoutConnector(
        this IServiceCollection services,
        IConfiguration configuration, 
        ServiceLifetime serviceLifetime = ServiceLifetime.Scoped,
        string sectionName = "MaibCheckoutApiClient")
    {
        services.Configure<ConnectorOptions>(c => configuration.GetSection(sectionName).Bind(c));
        services.AddHttpClient(CommonConstants.MaibClientName);
        services.Add(new ServiceDescriptor(typeof(IMaibCheckoutApiClient), typeof(MaibCheckoutApiClient),
            serviceLifetime));

        return services;
    }

    /// <summary>
    ///     Adds connector to merchant ecomm service.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configure">Delegate to configure connector options</param>
    /// <param name="serviceLifetime">The <see cref="T:Microsoft.Extensions.DependencyInjection.ServiceLifetime" /> of the service.</param>
    public static IServiceCollection AddMaibCheckoutConnector(
        this IServiceCollection services,
        Action<ConnectorOptions> configure, 
        ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    {
        services.AddOptions<ConnectorOptions>()
            .Configure(configure);
        
        services.AddHttpClient(CommonConstants.MaibClientName);
        services.Add(new ServiceDescriptor(typeof(IMaibCheckoutApiClient), typeof(MaibCheckoutApiClient),
            serviceLifetime));

        return services;
    }
    
    /// <summary>
    ///     Adds sandbox connector to merchant ecomm service.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="serviceLifetime">The <see cref="T:Microsoft.Extensions.DependencyInjection.ServiceLifetime" /> of the service.</param>
    /// <param name="sectionName">Name of the section.</param>
    public static IServiceCollection AddMaibCheckoutSandboxConnector(
        this IServiceCollection services,
        IConfiguration configuration,
        ServiceLifetime serviceLifetime = ServiceLifetime.Scoped,
        string sectionName = "MaibCheckoutSandboxApiClient")
    {
        services.Configure<ConnectorOptions>(c => configuration.GetSection(sectionName).Bind(c));
        services.AddHttpClient(CommonConstants.MaibSandboxClientName);
        services.Add(new ServiceDescriptor(typeof(IMaibCheckoutApiSandboxClient), typeof(MaibCheckoutApiSandboxClient),
            serviceLifetime));

        return services;
    }

    /// <summary>
    ///     Adds sandbox connector to merchant ecomm service.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configure">Delegate to configure connector options</param>
    /// <param name="serviceLifetime">The <see cref="T:Microsoft.Extensions.DependencyInjection.ServiceLifetime" /> of the service.</param>
    public static IServiceCollection AddMaibCheckoutSandboxConnector(
        this IServiceCollection services,
        Action<ConnectorOptions> configure, 
        ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    {
        services.AddOptions<ConnectorOptions>()
            .Configure(configure);
        
        services.AddHttpClient(CommonConstants.MaibSandboxClientName);
        services.Add(new ServiceDescriptor(typeof(IMaibCheckoutApiSandboxClient), typeof(MaibCheckoutApiSandboxClient),
            serviceLifetime));

        return services;
    }
}