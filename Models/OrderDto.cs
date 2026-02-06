using System.Text.Json.Serialization;
using Maib.Checkout.Api.Connector.Models.Enums;

namespace Maib.Checkout.Api.Connector.Models;

public sealed record OrderDto
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    
    /// <summary>
    /// Order description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    
    /// <summary>
    /// Order date
    /// </summary>
    [JsonPropertyName("date")]
    public DateTimeOffset? Date { get; set; }
    
    /// <summary>
    /// Order amount
    /// </summary>
    [JsonPropertyName("orderAmount")]
    public decimal? Amount { get; set; }
    
    /// <summary>
    /// Order currency
    /// </summary>
    [JsonPropertyName("orderCurrency")]
    public Currency? Currency { get; set; }
    
    /// <summary>
    /// Order delivery amount
    /// </summary>
    [JsonPropertyName("deliveryAmount")]
    public decimal? DeliveryAmount { get; set; }
    
    /// <summary>
    /// Order delivery currency
    /// </summary>
    [JsonPropertyName("deliveryCurrency")]
    public Currency? DeliveryCurrency { get; set; }
    
    /// <summary>
    /// The products or services ordered from the website/app
    /// </summary>
    [JsonPropertyName("items")]
    public IReadOnlyList<OrderItemDto>? Items { get; set; }
}