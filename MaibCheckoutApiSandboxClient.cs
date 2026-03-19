using Maib.Checkout.Api.Connector.Models;
using Maib.Checkout.Api.Connector.Options;
using Microsoft.Extensions.Options;

namespace Maib.Checkout.Api.Connector;

internal sealed class MaibCheckoutApiSandboxClient(
    IHttpClientFactory clientFactory,
    IOptionsMonitor<ConnectorOptions> optionsAccessor)
    : MaibCheckoutApiClient(clientFactory, optionsAccessor), IMaibCheckoutApiSandboxClient
{
    protected override string HttpClientName => CommonConstants.MaibSandboxClientName;
}