using System.Text.Json.Serialization;

namespace Maib.Checkout.Api.Connector.Models;

/// <summary>
///     Any action result
/// </summary>
public abstract record OperationResult
{
    /// <summary>
    /// Error list
    /// </summary>
    [JsonPropertyName("errors")]
    public IReadOnlyList<OperationError>? Errors { get; set; }

    /// <summary>
    /// The result is successful or not
    /// </summary>
    [JsonPropertyName("ok")]
    public bool Ok { get; set; }
}

/// <summary>
///     Generic operation result for any requests for Web API service and some MVC actions.
/// </summary>
public record OperationResult<TResult> : OperationResult
{
    /// <summary>
    ///     Result for server operation
    /// </summary>
    [JsonPropertyName("result")]
    public TResult? Result { get; set; }
}

/// <summary>
///     Generic operation result for empty requests for Web API service and some MVC actions.
/// </summary>
public record EmptyOperationResult : OperationResult
{
}