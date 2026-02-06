using System.Text.Json.Serialization;
using Maib.Checkout.Api.Connector.Models;
using Maib.Checkout.Api.Connector.Models.Enums;
using Maib.Checkout.Api.Connector.Models.Requests;
using Maib.Checkout.Api.Connector.Models.Responses;

namespace Maib.Checkout.Api.Connector.Serialization;

[JsonSourceGenerationOptions(
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    Converters = 
    [
        typeof(JsonStringEnumConverter<Currency>),
        typeof(JsonStringEnumConverter<Language>),
        typeof(JsonStringEnumConverter<CheckoutStatus>),
        typeof(JsonStringEnumConverter<CheckoutOrderField>),
        typeof(JsonStringEnumConverter<OrderOption>),
        typeof(JsonStringEnumConverter<PaymentRefundStatus>)
    ])
]

[JsonSerializable(typeof(BaseRequest))]
[JsonSerializable(typeof(GenerateTokenRequest))]
[JsonSerializable(typeof(CreateCheckoutRequest))]
[JsonSerializable(typeof(RefundPaymentRequest))]
[JsonSerializable(typeof(GetAllCheckoutsRequest))]
[JsonSerializable(typeof(GetCheckoutByIdRequest))]

[JsonSerializable(typeof(OperationResult<CreateCheckoutResponse>))]
[JsonSerializable(typeof(OperationResult<GenerateTokenResponse>))]
[JsonSerializable(typeof(OperationResult<RefundPaymentResponse>))]
[JsonSerializable(typeof(OperationResult<PagedList<CheckoutDto>>))]
[JsonSerializable(typeof(OperationResult<CheckoutDto>))]

[JsonSerializable(typeof(OrderItemDto))]
[JsonSerializable(typeof(OperationError))]
[JsonSerializable(typeof(OperationResult))]
[JsonSerializable(typeof(EmptyOperationResult))]

[JsonSerializable(typeof(Currency))]
[JsonSerializable(typeof(Language))]
[JsonSerializable(typeof(CheckoutStatus))]
[JsonSerializable(typeof(OrderOption))]
[JsonSerializable(typeof(PaymentRefundStatus))]
internal partial class ConnectorJsonSerializerContext : JsonSerializerContext
{
}