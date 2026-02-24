using System.Text.Json.Serialization;

namespace Maib.Checkout.Api.Connector.Models.Requests;

public sealed record RefundPaymentRequest : BaseRequest
{
    protected override string Action => $"/payments/{PayId}/refund";

    /// <summary>
    /// Payment ID
    /// </summary>
    [JsonPropertyName("payId")]
    public string PayId { get; set; } = null!;
    
    /// <summary>
    /// Refund amount. If 'null' full refund will be requested
    /// </summary>
    [JsonPropertyName("amount")]
    public decimal? Amount { get; set; }
    
    /// <summary>
    /// Refund operation reason
    /// </summary>
    [JsonPropertyName("reason")]
    public string? Reason { get; set; }
}