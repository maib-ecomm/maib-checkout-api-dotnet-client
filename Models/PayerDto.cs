using System.Text.Json.Serialization;

namespace Maib.Checkout.Api.Connector.Models;

public sealed record PayerDto
{
    /// <summary>
    /// Payer name
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    /// <summary>
    /// Payer email
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    
    /// <summary>
    /// Payer phone
    /// </summary>
    [JsonPropertyName("phone")]
    public string? Phone { get; set; }
    
    /// <summary>
    /// Payer ip
    /// </summary>
    [JsonPropertyName("ip")]
    public string? Ip { get; set; }
    
    /// <summary>
    /// Payer user agent
    /// </summary>
    [JsonPropertyName("userAgent")]
    public string? UserAgent { get; set; }
}