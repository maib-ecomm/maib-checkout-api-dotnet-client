using System.Text.Json.Serialization;
using Maib.Checkout.Api.Connector.Models.Enums;

namespace Maib.Checkout.Api.Connector.Models.Responses;

public sealed record CheckoutDto
{
    /// <summary>
    /// Checkout ID
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Checkout creation date
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Checkout status
    /// </summary>
    [JsonPropertyName("status")]
    public CheckoutStatus Status { get; set; }

    /// <summary>
    /// Checkout amount
    /// </summary>
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }
    
    /// <summary>
    /// The URL to which the merchant will receive a callback about the completed payment
    /// </summary>
    [JsonPropertyName("callbackUrl")]
    public string? CallbackUrl { get; set; }

    /// <summary>
    /// The URL to which the payer will be redirected after successful payment
    /// </summary>
    [JsonPropertyName("successUrl")]
    public string? SuccessUrl { get; set; }

    /// <summary>
    /// The URL to which the payer will be redirected after failed payment
    /// </summary>
    [JsonPropertyName("failUrl")]
    public string? FailUrl { get; set; }
    
    /// <summary>
    /// Checkout currency
    /// </summary>
    [JsonPropertyName("currency")]
    public Currency Currency { get; set; }
    
    /// <summary>
    /// The language in which the checkout page is displayed
    /// </summary>
    [JsonPropertyName("language")]
    public Language Language { get; set; }

    /// <summary>
    /// Checkout url
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = null!;

    /// <summary>
    /// Checkout order information
    /// </summary>
    [JsonPropertyName("order")]
    public OrderDto? Order { get; set; }
    
    /// <summary>
    /// Checkout payer information
    /// </summary>
    [JsonPropertyName("payer")]
    public PayerDto? Payer { get; set; }
    
    /// <summary>
    /// Checkout completion date
    /// </summary>
    [JsonPropertyName("completedAt")]
    public DateTimeOffset? CompletedAt { get; set; }
    
    /// <summary>
    /// Checkout failure date
    /// </summary>
    [JsonPropertyName("failedAt")]
    public DateTimeOffset? FailedAt { get; set; }

    /// <summary>
    /// Checkout cancellation date
    /// </summary>
    [JsonPropertyName("cancelledAt")]
    public DateTimeOffset? CancelledAt { get; set; }
    
    /// <summary>
    /// Checkout expiration date
    /// </summary>
    [JsonPropertyName("expiresAt")]
    public DateTimeOffset ExpiresAt { get; set; }

    /// <summary>
    /// Checkout payment
    /// </summary>
    [JsonPropertyName("payment")]
    public PaymentDto? Payment { get; set; }
}