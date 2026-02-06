using System.Text.Json.Serialization;
using Maib.Checkout.Api.Connector.Models.Enums;

namespace Maib.Checkout.Api.Connector.Models;

public sealed record PaymentDto
{
    /// <summary>
    /// Payment ID
    /// </summary>
    [JsonPropertyName("name")]
    public Guid PaymentId { get; set; }

    /// <summary>
    /// Payment execution date
    /// </summary>
    [JsonPropertyName("executedAt")]
    public DateTimeOffset ExecutedAt { get; set; }

    /// <summary>
    /// Payment status
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = null!;

    /// <summary>
    /// Payment amount
    /// </summary>
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Payment currency
    /// </summary>
    [JsonPropertyName("currency")]
    public Currency Currency { get; set; }

    /// <summary>
    /// Payment type, possible values: [A2A, MMC, MIA]
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = null!;

    /// <summary>
    /// Payment provider type [QR, Rrp, Maibank, MMC]
    /// </summary>
    [JsonPropertyName("providerType")]
    public string ProviderType { get; set; } = null!;

    /// <summary>
    /// Payer name
    /// </summary>
    [JsonPropertyName("senderName")]
    public string? SenderName { get; set; }

    /// <summary>
    /// Payer IBAN
    /// </summary>
    [JsonPropertyName("senderIban")]
    public string? SenderIban { get; set; }

    /// <summary>
    /// Recipient IBAN
    /// </summary>
    [JsonPropertyName("recipientIban")]
    public string? RecipientIban { get; set; }

    /// <summary>
    /// Payment reference number
    /// </summary>
    [JsonPropertyName("referenceNumber")]
    public string ReferenceNumber { get; set; } = null!;
    
    /// <summary>
    /// Payment approval code
    /// </summary>
    [JsonPropertyName("approvalCode")]
    public string? ApprovalCode { get; set; }

    /// <summary>
    /// Merchant category code
    /// </summary>
    [JsonPropertyName("mcc")]
    public string Mcc { get; set; } = null!;

    /// <summary>
    /// Order ID
    /// </summary>
    [JsonPropertyName("orderId")]
    public string? OrderId { get; set; }

    /// <summary>
    /// Terminal ID
    /// </summary>
    [JsonPropertyName("terminalId")]
    public string? TerminalId { get; set; }

    /// <summary>
    /// Total amount that was refunded
    /// </summary>
    [JsonPropertyName("refundedAmount")]
    public decimal RefundedAmount { get; set; }

    /// <summary>
    /// The method by which payment was made
    /// </summary>
    [JsonPropertyName("paymentMethod")]
    public string? PaymentMethod { get; set; }

    /// <summary>
    /// The refund amount that has been requested but may not yet have been approved
    /// </summary>
    [JsonPropertyName("requestedRefundAmount")]
    public decimal RequestedRefundAmount { get; set; }

    /// <summary>
    /// First refund operation date
    /// </summary>
    [JsonPropertyName("firstRefundedAt")]
    public DateTimeOffset? FirstRefundedAt { get; set; }

    /// <summary>
    /// Last refund operation date
    /// </summary>
    [JsonPropertyName("lastRefundedAt")]
    public DateTimeOffset? LastRefundedAt { get; set; }

    /// <summary>
    /// Payer's payment notes
    /// </summary>
    [JsonPropertyName("note")]
    public string? Note { get; set; }
}