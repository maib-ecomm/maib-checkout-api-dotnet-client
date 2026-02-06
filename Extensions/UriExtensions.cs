using System.Collections.Specialized;
using System.Web;

namespace Maib.Checkout.Api.Connector.Extensions;

public static class UriExtensions
{
    public static Uri AppendQueryParams(this Uri uri, IDictionary<string, string?> queryParams)
    {
        var uriBuilder = new UriBuilder(uri);

        NameValueCollection baseQuery = HttpUtility.ParseQueryString(uriBuilder.Query);

        foreach (var (key, value) in queryParams)
        {
            if (string.IsNullOrWhiteSpace(key))
                continue;

            if (string.IsNullOrWhiteSpace(value))
                continue;

            baseQuery.Add(key, value);
        }

        uriBuilder.Query = baseQuery.ToString();

        return uriBuilder.Uri;
    }
}