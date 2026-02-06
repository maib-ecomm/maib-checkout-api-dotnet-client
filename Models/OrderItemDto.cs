using System.Text.Json.Serialization;
using Maib.Checkout.Api.Connector.Models.Enums;

namespace Maib.Checkout.Api.Connector.Models;

public sealed record OrderItemDto
{
    /// <summary>
    /// Product ID
    /// </summary>
    [JsonPropertyName("externalId")]
    public string? Id { get; set; }

    /// <summary>
    /// Product name
    /// </summary>
    [JsonPropertyName("title")]
    public string? Name { get; set; }

    /// <summary>
    /// Product price
    /// </summary>
    [JsonPropertyName("amount")]
    public decimal? Price { get; set; }
    
    /// <summary>
    /// Product currency
    /// </summary>
    [JsonPropertyName("currency")]
    public Currency? Currency { get; set; }

    /// <summary>
    /// Product quantity
    /// </summary>
    [JsonPropertyName("quantity")]
    public decimal? Quantity { get; set; }
    
    /// <summary>
    /// Product display order
    /// </summary>
    [JsonPropertyName("displayOrder")]
    public int? DisplayOrder { get; set; }
}