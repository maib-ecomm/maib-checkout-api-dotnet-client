using System.Globalization;
using System.Text;
using Maib.Checkout.Api.Connector.Extensions;
using Maib.Checkout.Api.Connector.Models.Enums;

namespace Maib.Checkout.Api.Connector.Models.Requests;

public sealed record GetAllCheckoutsRequest : BaseRequest
{
    protected override string Action => "/checkouts";
    
    /// <summary>
    /// Gets or sets the HTTP method.
    /// </summary>
    protected override HttpMethod HttpMethod { get; } = HttpMethod.Get;
    
    /// <summary>
    /// Checkout ID filter
    /// </summary>
    public Guid? Id { get; set; }
    
    /// <summary>
    /// Checkout order id filter
    /// </summary>
    public string? OrderId { get; set; }

    /// <summary>
    /// Checkout status filter
    /// </summary>
    public CheckoutStatus? Status { get; set; }

    /// <summary>
    /// Checkout minimal amount filter
    /// </summary>
    public decimal? MinAmount { get; set; }

    /// <summary>
    /// Checkout maximum amount filter
    /// </summary>
    public decimal? MaxAmount { get; set; }

    /// <summary>
    /// Checkout currency filter
    /// </summary>
    public Currency? Currency { get; set; }
    
    /// <summary>
    /// Checkout language filter
    /// </summary>
    public Language? Language { get; set; }

    /// <summary>
    /// Checkout payer name filter
    /// </summary>
    public string? PayerName { get; set; }
    
    /// <summary>
    /// Checkout payer email filter
    /// </summary>
    public string? PayerEmail { get; set; }
    
    /// <summary>
    /// Checkout payer phone filter
    /// </summary>
    public string? PayerPhone { get; set; }
    
    /// <summary>
    /// Checkout payer ip address filter
    /// </summary>
    public string? PayerIp { get; set; }

    /// <summary>
    /// Checkout created at from filter
    /// </summary>
    public DateTimeOffset? CreatedAtFrom { get; set; }
    
    /// <summary>
    /// Checkout created at to filter
    /// </summary>
    public DateTimeOffset? CreatedAtTo { get; set; }
    
    /// <summary>
    /// Checkout expires at from filter
    /// </summary>
    public DateTimeOffset? ExpiresAtFrom { get; set; }
    
    /// <summary>
    /// Checkout expires at to filter
    /// </summary>
    public DateTimeOffset? ExpiresAtTo { get; set; }
    
    /// <summary>
    /// Checkout cancelled at from filter
    /// </summary>
    public DateTimeOffset? CancelledAtFrom { get; set; }
    
    /// <summary>
    /// Checkout cancelled at to filter
    /// </summary>
    public DateTimeOffset? CancelledAtTo { get; set; }
    
    /// <summary>
    /// Checkout failed at from filter
    /// </summary>
    public DateTimeOffset? FailedAtFrom { get; set; }
    
    /// <summary>
    /// Checkout failed at to filter
    /// </summary>
    public DateTimeOffset? FailedAtTo { get; set; }
    
    /// <summary>
    /// Checkout completed at from filter
    /// </summary>
    public DateTimeOffset? CompletedAtFrom { get; set; }
    
    /// <summary>
    /// Checkout completed at to filter
    /// </summary>
    public DateTimeOffset? CompletedAtTo { get; set; }

    /// <summary>
    /// Count of records per request
    /// </summary>
    public int? Count { get; set; }
    
    /// <summary>
    /// Offset records per request
    /// </summary>
    public int? Offset { get; set; }
    
    /// <summary>
    /// Sorting property (CreatedAt by default)
    /// </summary>
    public CheckoutOrderField? SortBy { get; set; }
    
    /// <summary>
    /// Ordering option: Asc/Desc
    /// </summary>
    public OrderOption? Order { get; set; }

    /// <summary>
    /// Converts to <see cref="HttpRequestMessage" />.
    /// </summary>
    /// <param name="url">The URL.</param>
    internal override HttpRequestMessage ToHttpRequest(string url)
    {
        var uri = new Uri(url + Action).AppendQueryParams(ToQueryParams());
        return new HttpRequestMessage(HttpMethod, uri);
    }

    private Dictionary<string, string?> ToQueryParams()
    {
        var qp = new Dictionary<string, string?>(StringComparer.Ordinal)
        {
            ["orderId"] = TrimOrNull(OrderId),
            ["payerName"] = TrimOrNull(PayerName),
            ["payerEmail"] = TrimOrNull(PayerEmail),
            ["payerPhone"] = TrimOrNull(PayerPhone),
            ["payerIp"] = TrimOrNull(PayerIp),
            ["id"] = Id?.ToString(),
            ["status"] = Status?.ToString(),
            ["currency"] = Currency?.ToString(),
            ["language"] = Language?.ToString(),
            ["sortBy"] = SortBy?.ToString(),
            ["order"] = Order?.ToString(),
            ["minAmount"] = MinAmount?.ToString(CultureInfo.InvariantCulture),
            ["maxAmount"] = MaxAmount?.ToString(CultureInfo.InvariantCulture),
            ["count"] = Count?.ToString(CultureInfo.InvariantCulture),
            ["offset"] = Offset?.ToString(CultureInfo.InvariantCulture),
            ["createdAtFrom"] = ToIso(CreatedAtFrom),
            ["createdAtTo"] = ToIso(CreatedAtTo),
            ["expiresAtFrom"] = ToIso(ExpiresAtFrom),
            ["expiresAtTo"] = ToIso(ExpiresAtTo),
            ["cancelledAtFrom"] = ToIso(CancelledAtFrom),
            ["cancelledAtTo"] = ToIso(CancelledAtTo),
            ["failedAtFrom"] = ToIso(FailedAtFrom),
            ["failedAtTo"] = ToIso(FailedAtTo),
            ["completedAtFrom"] = ToIso(CompletedAtFrom),
            ["completedAtTo"] = ToIso(CompletedAtTo)
        };

        return qp;

        static string? TrimOrNull(string? value)
            => string.IsNullOrWhiteSpace(value) ? null : value.Trim();

        static string? ToIso(DateTimeOffset? value)
            => value?.ToString("O", CultureInfo.InvariantCulture);
    }
}