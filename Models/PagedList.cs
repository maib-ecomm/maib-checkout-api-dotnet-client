namespace Maib.Checkout.Api.Connector.Models;

public sealed record PagedList<T>
{
    /// <summary>Gets or sets the items.</summary>
    public T[] Items { get; set; } = null!;

    /// <summary>Gets the items count.</summary>
    public int Count { get; set; }

    /// <summary>Gets or sets the total count.</summary>
    public int TotalCount { get; set; }
}
