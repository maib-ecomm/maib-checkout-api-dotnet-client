using System.Net.Http.Headers;
using System.Text.Json;
using Maib.Checkout.Api.Connector.Exceptions;
using Maib.Checkout.Api.Connector.Models;
using Maib.Checkout.Api.Connector.Models.Requests;
using Maib.Checkout.Api.Connector.Models.Responses;
using Maib.Checkout.Api.Connector.Options;
using Maib.Checkout.Api.Connector.Serialization;
using Microsoft.Extensions.Options;

namespace Maib.Checkout.Api.Connector;

internal sealed class MaibCheckoutApiClient : IMaibCheckoutApiClient
{
    public const string HttpClientName = "MaibCheckoutApiClient";

    private readonly IHttpClientFactory _clientFactory;
    private ConnectorOptions _options;

    public MaibCheckoutApiClient(IHttpClientFactory clientFactory, IOptionsMonitor<ConnectorOptions> optionsAccessor)
    {
        _clientFactory = clientFactory;

        _options = optionsAccessor.CurrentValue;
        optionsAccessor.OnChange((options, _) => _options = options);
    }

    public async Task<OperationResult<RefundPaymentResponse?>> RefundPaymentAsync(RefundPaymentRequest request, CancellationToken cancellationToken = default)
    {
        return await SendAsync<OperationResult<RefundPaymentResponse?>, RefundPaymentRequest>(request, cancellationToken);
    }

    public async Task<OperationResult<CreateCheckoutResponse?>> CreateCheckoutAsync(CreateCheckoutRequest request, CancellationToken cancellationToken = default)
    {
        return await SendAsync<OperationResult<CreateCheckoutResponse?>, CreateCheckoutRequest>(request, cancellationToken);
    }

    public async Task<OperationResult<GenerateTokenResponse?>> GenerateTokenAsync(GenerateTokenRequest request, CancellationToken cancellationToken = default)
    {
        return await SendAsync<OperationResult<GenerateTokenResponse?>, GenerateTokenRequest>(request, cancellationToken);
    }

    public async Task<OperationResult<PagedList<CheckoutDto>>> GetAllCheckoutsAsync(GetAllCheckoutsRequest request, CancellationToken cancellationToken = default)
    {
        return await SendAsync<OperationResult<PagedList<CheckoutDto>>, GetAllCheckoutsRequest>(request, cancellationToken);
    }

    public async Task<OperationResult<CheckoutDto>> GetCheckoutByIdAsync(GetCheckoutByIdRequest request, CancellationToken cancellationToken = default)
    {
        return await SendAsync<OperationResult<CheckoutDto>, GetCheckoutByIdRequest>(request, cancellationToken);
    }

    private async Task<TResponse> SendAsync<TResponse, TRequest>(TRequest requestData,
        CancellationToken cancellationToken = default)
        where TRequest : BaseRequest
    {
        if (requestData == null)
            throw new ArgumentNullException();

        using HttpRequestMessage requestMessage = requestData.ToHttpRequest(_options.Url);

        if (!string.IsNullOrWhiteSpace(requestData.AccessToken))
        {
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", requestData.AccessToken);
        }

        using HttpResponseMessage responseMessage =
            await SendHttpAsync(requestMessage, cancellationToken: cancellationToken);

        var responseJson = await responseMessage.Content.ReadAsStringAsync();
        var httpStatusCode = Convert.ToInt32(responseMessage.StatusCode);

        return ParseResponse<TResponse>(httpStatusCode, responseJson);
    }

    private async Task<HttpResponseMessage> SendHttpAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        using HttpClient client = CreateClient();

        try
        {
            return await client.SendAsync(request, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new NotReachableException(
                $"Could not make request to service at '{_options.Url}'. " +
                $"The provided base url may be incorrect or there may be a firewall blocking the port. Request timeout = {client.Timeout}",
                ex);
        }
    }

    private HttpClient CreateClient()
    {
        HttpClient client = _clientFactory.CreateClient(HttpClientName);
        client.Timeout = TimeSpan.FromMilliseconds(_options.RequestTimeoutMs);

        return client;
    }

    private static TResponse ParseResponse<TResponse>(int httpStatusCode, string responseJson)
    {
        if (httpStatusCode is not (200 or 400 or 401 or 403 or 404 or 409 or 500))
            throw new InvalidResponseException(httpStatusCode, responseJson);
        
        if (string.IsNullOrWhiteSpace(responseJson))
        {
            throw new StatusCodeException(httpStatusCode);
        }

        try
        {
            TResponse? result = JsonSerializer.Deserialize<TResponse>(responseJson,
                ConnectorJsonSerializerContext.Default.Options);

            if (result == null)
                throw new InvalidResponseException(httpStatusCode, responseJson);

            return result;
        }
        catch (JsonException ex)
        {
            throw new InvalidResponseException(httpStatusCode, responseJson, ex);
        }
    }
}