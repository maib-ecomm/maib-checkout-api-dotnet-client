using System.Text.Json.Serialization;
using Maib.Checkout.Api.Connector.Models.Enums;

namespace Maib.Checkout.Api.Connector.Models.Responses;

public sealed record RefundPaymentResponse
{
    /// <summary>
    /// Refund ID
    /// </summary>
    [JsonPropertyName("refundId")]
    public Guid RefundId { get; set; }

    /// <summary>
    /// Refund status
    /// </summary>
    [JsonPropertyName("status")]
    public PaymentRefundStatus Status { get; set; }
}