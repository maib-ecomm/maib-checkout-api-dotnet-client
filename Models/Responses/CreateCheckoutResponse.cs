using System;
using System.Text.Json.Serialization;

namespace Maib.Checkout.Api.Connector.Models.Responses;

public sealed record CreateCheckoutResponse
{
    /// <summary>
    /// Checkout identifier assigned by maib ecomm
    /// </summary>
    [JsonPropertyName("checkoutId")]
    public Guid CheckoutId { get; set; }

    /// <summary>
    /// The link to the maib ecomm checkout page where the Customer must be redirected to complete the payment
    /// </summary>
    [JsonPropertyName("checkoutUrl")]
    public string CheckoutUrl { get; set; } = null!;
}

