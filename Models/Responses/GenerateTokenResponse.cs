using System.Text.Json.Serialization;

namespace Maib.Checkout.Api.Connector.Models.Responses;

public sealed record GenerateTokenResponse
{
    /// <summary>
    /// Access Token
    /// </summary>
    [JsonPropertyName("accessToken")]
    public string AccessToken { get; set; } = null!;

    /// <summary>
    /// Access Token lifetime in seconds
    /// </summary>

    [JsonPropertyName("expiresIn")]
    public int ExpiresIn { get; set; }

    /// <summary>
    /// Token type (Bearer)
    /// </summary>
    [JsonPropertyName("tokenType")]
    public string TokenType { get; set; } = null!;
}