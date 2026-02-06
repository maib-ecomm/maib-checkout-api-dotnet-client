namespace Maib.Checkout.Api.Connector.Models.Requests;

public sealed record GetCheckoutByIdRequest : BaseRequest
{
    protected override string Action => $"/checkouts/{CheckoutId}";
    
    /// <summary>
    /// Checkout ID
    /// </summary>
    public Guid CheckoutId { get; set; }
    
    /// <summary>
    /// Gets or sets the HTTP method.
    /// </summary>
    protected override HttpMethod HttpMethod { get; } = HttpMethod.Get;

    /// <summary>
    /// Converts to <see cref="HttpRequestMessage" />.
    /// </summary>
    /// <param name="url">The URL.</param>
    internal override HttpRequestMessage ToHttpRequest(string url)
    => new(HttpMethod, url + Action);
}