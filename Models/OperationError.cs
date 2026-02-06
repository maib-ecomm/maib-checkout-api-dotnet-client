using System.Text.Json.Serialization;

namespace Maib.Checkout.Api.Connector.Models;

public record OperationError
{
    /// <summary>
    ///     Gets the error code
    /// </summary>
    [JsonPropertyName("errorCode")]
    public string ErrorCode { get; set; } = null!;

    /// <summary>
    ///     Gets the error message
    /// </summary>
    [JsonPropertyName("errorMessage")]
    public string ErrorMessage { get; set; } = null!;

    /// <summary>
    /// Details of the error
    /// </summary>
    [JsonPropertyName("errorArgs")]
    public Dictionary<string, string>? ErrorArgs { get; set; }
}