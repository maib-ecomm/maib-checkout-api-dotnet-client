using System.Text.Json.Serialization;

namespace Maib.Checkout.Api.Connector.Models.Requests;

public sealed record GenerateTokenRequest : BaseRequest
{
    protected override string Action => "/auth/token";

    /// <summary>
    /// Client ID
    /// </summary>
    [JsonPropertyName("clientId")]
    public string ClientId { get; set; } = null!;
    
    /// <summary>
    /// Client Secret
    /// </summary>
    [JsonPropertyName("clientSecret")]
    public string ClientSecret { get; set; } = null!;
}