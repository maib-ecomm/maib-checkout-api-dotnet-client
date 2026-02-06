[![maib](https://www.maib.md/images/logo.svg)](https://www.maib.md)

# maib Checkout API .NET Client

**maib** e-commerce Checkout API docs: https://docs.maibmerchants.md/checkout

## Contents
- [Introduction](#introduction)
- [Requirements](#requirements)
- [Before usage](#before-usage)
- [Usage](#usage)
- [Examples](#examples)
- [Troubleshooting](#troubleshooting)

## Introduction

The maib Checkout API .NET client is a .NET (Core) library designed to simplify integration with the maib e-commerce Checkout API.

The client provides 4 main methods:
- **Create a new checkout** (`CreateCheckoutAsync`) — creates a new checkout session and returns a *RedirectUrl* where the customer should be redirected to complete the payment.
- **Get all checkouts** (`GetAllCheckoutsAsync`) — returns checkouts using various filters.
- **Get a checkout by ID** (`GetCheckoutByIdAsync`) — returns checkout information by its unique identifier.
- **Refund a payment** (`RefundPaymentAsync`) — requests a refund for a transaction made for a specific checkout. After the refund operation is completed, the success/failed result will be sent to the *CallbackUrl* specified in the request.

The `GenerateTokenAsync` operation is used to acquire an access token, which is required for other operations.

## Requirements

- Any .NET implementation compatible with .NET Standard (see compatibility table): https://learn.microsoft.com/en-us/dotnet/standard/net-standard?tabs=net-standard-2-1

## Before usage

To perform any operations, you need to obtain `ClientId` and `ClientSecret`.

Please send a message to **ecomm@maib.md** to complete the registration process and receive your credentials.

## Usage

1) Add the namespace in `Program.cs`:

```csharp
using Maib.Checkout.Api.Connector.Extensions;
```

2) Register the client:

```csharp
services.AddMaibCheckoutConnector({IConfiguration instance}, {sectionName});
```

Configuration must contain the following section:

```json
"sectionName": {
  "Url": "OurApiUrl",
  "RequestTimeoutMs": 30000
}
```

3) Dependency injection:

```csharp
public class YourService
{
    private readonly IMaibCheckoutApiClient _client;

    public YourService(IMaibCheckoutApiClient client)
    {
        _client = client;
    }
}
```

## Examples

### Access token generation

```csharp
var generateTokenRequest = new GenerateTokenRequest
{
    ClientId = "YourProjectId",
    ClientSecret = "YourProjectSecret"
};

var generateTokenResult = await _client.GenerateTokenAsync(generateTokenRequest, cancellationToken);

if (!generateTokenResult.Ok)
{
    // request failed
    foreach (var error in generateTokenResult.Errors)
    {
        _logger.LogError(error.ErrorMessage);
    }

    return;
}

var accessToken = generateTokenResult.Result!.AccessToken;

// process response ...
```

### Create checkout

```csharp
var request = new CreateCheckoutRequest
{
    AccessToken = accessToken,
    Amount = 22.50m,
    Currency = Currency.MDL,

    CallbackUrl = "https://example.com/callback", // optional
    FailUrl = "https://example.com/fail",         // optional
    SuccessUrl = "https://example.com/success",   // optional
    Language = Language.Ro,                       // optional

    Payer = new PayerDto // optional
    {
        Name = "John Smith",
        Phone = "37369222222",
        Email = "test.email@gmail.com",
        UserAgent = "Chrome",
        Ip = "192.193.245.11"
    },

    Order = new OrderDto // optional
    {
        Amount = 19.52m,
        Currency = Currency.MDL,
        DeliveryAmount = 21.43m,
        DeliveryCurrency = Currency.MDL,
        Description = "order description",
        Id = "TY223423",
        Items =
        [
            new OrderItemDto
            {
                Name = "Item1",
                Currency = Currency.MDL,
                Price = 10.23m,
                Quantity = 1,
                DisplayOrder = 1,
                Id = "32423422"
            },
            new OrderItemDto
            {
                Name = "Item2",
                Currency = Currency.MDL,
                Price = 11.25m,
                Quantity = 2,
                DisplayOrder = 3,
                Id = "453434345"
            }
        ]
    }
};

var createCheckoutResult = await _client.CreateCheckoutAsync(request, cancellationToken);

if (!createCheckoutResult.Ok)
{
    // request failed
    foreach (var error in createCheckoutResult.Errors)
    {
        _logger.LogError(error.ErrorMessage);
    }

    return;
}

var checkoutId = createCheckoutResult.Result!.CheckoutId;
var checkoutUrl = createCheckoutResult.Result!.CheckoutUrl;

// process create checkout result ...
```

### Refund request

```csharp
var request = new RefundPaymentRequest
{
    PayId = "0F2442CE-7738-4AEA-AD08-54F2E7321086", // Payment identifier received in the callback message
    AccessToken = accessToken,
    Amount = 23.44m,                    // optional; if not specified, full refund will be requested
    Reason = "Some reason for refund",   // optional
    CallbackUrl = "https://www.example.com/callback" // optional; refund completion callback URL
};

var refundResult = await _client.RefundPaymentAsync(request, cancellationToken);

if (!refundResult.Ok)
{
    // request failed
    foreach (var error in refundResult.Errors)
    {
        _logger.LogError(error.ErrorMessage);
    }

    return;
}

var refundId = refundResult.Result!.RefundId;
var status = refundResult.Result!.Status; // PaymentRefundStatus.Created will be returned on successful refund request.

// process refund result ...
```

### Get all checkouts request

```csharp
// All filtering properties are optional; if none are specified then all checkouts will be returned.
var getAllRequest = new GetAllCheckoutsRequest
{
    Id = Guid.Parse("2f6b3d5e-7b2a-4f2d-9c6b-1a4c8f0f3d21"),
    OrderId = "ORD-2026-000123",

    Status = CheckoutStatus.Created,
    MinAmount = 10.50m,
    MaxAmount = 2500.00m,

    Currency = Currency.MDL,
    Language = Language.Ro,

    PayerName = "John Doe",
    PayerEmail = "john.doe@example.com",
    PayerPhone = "+37369000111",
    PayerIp = "203.0.113.42",

    CreatedAtFrom = DateTimeOffset.UtcNow.AddDays(-14),
    CreatedAtTo = DateTimeOffset.UtcNow,

    ExpiresAtFrom = DateTimeOffset.UtcNow.AddDays(1),
    ExpiresAtTo = DateTimeOffset.UtcNow.AddDays(7),

    CancelledAtFrom = DateTimeOffset.UtcNow.AddDays(-30),
    CancelledAtTo = DateTimeOffset.UtcNow.AddDays(-20),

    FailedAtFrom = DateTimeOffset.UtcNow.AddDays(-10),
    FailedAtTo = DateTimeOffset.UtcNow.AddDays(-5),

    CompletedAtFrom = DateTimeOffset.UtcNow.AddDays(-4),
    CompletedAtTo = DateTimeOffset.UtcNow.AddDays(-1),

    Count = 50,
    Offset = 0,

    SortBy = CheckoutOrderField.CreatedAt,
    Order = OrderOption.Desc,

    AccessToken = accessToken // required
};

var checkoutsResult = await _client.GetAllCheckoutsAsync(getAllRequest, cancellationToken);

if (!checkoutsResult.Ok)
{
    // request failed
    foreach (var error in checkoutsResult.Errors)
    {
        _logger.LogError(error.ErrorMessage);
    }

    return;
}

CheckoutDto[] checkouts = checkoutsResult.Result!.Items;

PaymentDto[] payments = checkouts
    .Where(x => x.Payment != null)
    .Select(x => x.Payment!)
    .ToArray(); // If a checkout has already been paid, CheckoutDto contains PaymentDto with payment info.

int count = checkoutsResult.Result!.Count;            // Count of returned checkouts
int totalCount = checkoutsResult.Result!.TotalCount;  // Total count matching specified filters

// process get all checkouts result ...
```

### Get checkout by id

```csharp
var getByIdRequest = new GetCheckoutByIdRequest
{
    AccessToken = accessToken,
    CheckoutId = Guid.Parse("ee3c152a-d94b-414b-9d5b-68839a8f415a")
};

var checkoutResponse = await _client.GetCheckoutByIdAsync(getByIdRequest, cancellationToken);

if (!checkoutResponse.Ok)
{
    // request failed
    foreach (var error in checkoutResponse.Errors)
    {
        _logger.LogError(error.ErrorMessage);
    }

    return;
}

// Base checkout information
var checkoutId = checkoutResponse.Result!.Id;
var checkoutCreatedAt = checkoutResponse.Result!.CreatedAt;
var checkoutStatus = checkoutResponse.Result!.Status;
var checkoutAmount = checkoutResponse.Result!.Amount;
var checkoutCurrency = checkoutResponse.Result!.Currency;
var checkoutOrder = checkoutResponse.Result!.Order;
var checkoutExpiresAt = checkoutResponse.Result!.ExpiresAt;
var checkoutPayer = checkoutResponse.Result!.Payer;
var checkoutPayment = checkoutResponse.Result!.Payment;
var checkoutCallbackUrl = checkoutResponse.Result!.CallbackUrl;
var checkoutSuccessUrl = checkoutResponse.Result!.SuccessUrl;
var checkoutFailUrl = checkoutResponse.Result!.FailUrl;
var checkoutLanguage = checkoutResponse.Result!.Language;
var checkoutUrl = checkoutResponse.Result!.Url;
var checkoutCompletedAt = checkoutResponse.Result!.CompletedAt;
var checkoutFailedAt = checkoutResponse.Result!.FailedAt;
var checkoutCancelledAt = checkoutResponse.Result!.CancelledAt;

// Order info
var orderId = checkoutResponse.Result!.Order.Id;
var orderDescription = checkoutResponse.Result!.Order.Description;
var orderDate = checkoutResponse.Result!.Order.Date;
var orderDeliveryAmount = checkoutResponse.Result!.Order.DeliveryAmount;
var orderDeliveryCurrency = checkoutResponse.Result!.Order.DeliveryCurrency;
var orderAmount = checkoutResponse.Result!.Order.Amount;
var orderCurrency = checkoutResponse.Result!.Order.Currency;
var orderItems = checkoutResponse.Result!.Order.OrderItems;

// Order items (1st)
var orderItem1 = checkoutResponse.Result!.Order.OrderItems[0];
var orderItem1ExternalId = orderItem1.ExternalId;
var orderItem1Title = orderItem1.Title;
var orderItem1Amount = orderItem1.Amount;
var orderItem1Currency = orderItem1.Currency;
var orderItem1Quantity = orderItem1.Quantity;
var orderItem1DisplayOrder = orderItem1.DisplayOrder;

// Order items (2nd)
var orderItem2 = checkoutResponse.Result!.Order.OrderItems[1];
var orderItem2ExternalId = orderItem2.ExternalId;
var orderItem2Title = orderItem2.Title;
var orderItem2Amount = orderItem2.Amount;
var orderItem2Currency = orderItem2.Currency;
var orderItem2Quantity = orderItem2.Quantity;
var orderItem2DisplayOrder = orderItem2.DisplayOrder;

// Payer info
var payerName = checkoutResponse.Result!.Payer.Name;
var payerEmail = checkoutResponse.Result!.Payer.Email;
var payerUserAgent = checkoutResponse.Result!.Payer.UserAgent;
var payerPhone = checkoutResponse.Result!.Payer.Phone;
var payerIp = checkoutResponse.Result!.Payer.Ip;

// Payment info
var paymentPaymentId = checkoutResponse.Result!.Payment?.PaymentId;
var paymentPaymentMethod = checkoutResponse.Result!.Payment?.PaymentMethod;
var paymentNote = checkoutResponse.Result!.Payment?.Note;
var paymentExecutedAt = checkoutResponse.Result!.Payment?.ExecutedAt;
var paymentStatus = checkoutResponse.Result!.Payment?.Status;
var paymentAmount = checkoutResponse.Result!.Payment?.Amount;
var paymentCurrency = checkoutResponse.Result!.Payment?.Currency;
var paymentType = checkoutResponse.Result!.Payment?.Type;
var paymentProviderType = checkoutResponse.Result!.Payment?.ProviderType;
var paymentSenderName = checkoutResponse.Result!.Payment?.SenderName;
var paymentSenderIban = checkoutResponse.Result!.Payment?.SenderIban;
var paymentRecipientIban = checkoutResponse.Result!.Payment?.RecipientIban;
var paymentReferenceNumber = checkoutResponse.Result!.Payment?.ReferenceNumber;
var paymentMcc = checkoutResponse.Result!.Payment?.Mcc;
var paymentOrderId = checkoutResponse.Result!.Payment?.OrderId;
var paymentTerminalId = checkoutResponse.Result!.Payment?.TerminalId;
var paymentRefundedAmount = checkoutResponse.Result!.Payment?.RefundedAmount;
var paymentApprovalCode = checkoutResponse.Result!.Payment?.ApprovalCode;
var paymentRequestedRefundAmount = checkoutResponse.Result!.Payment?.RequestedRefundAmount;
var paymentFirstRefundedAt = checkoutResponse.Result!.Payment?.FirstRefundedAt;
var paymentLastRefundedAt = checkoutResponse.Result!.Payment?.LastRefundedAt;

// process checkout response ...
```

## Troubleshooting

All requests return the following result structure:

```json
{
  "Result": {},
  "Ok": true,
  "Errors": []
}
```

### Properties
- **Ok** — indicates whether the request is successful. A successful response is when `Result` is not null and `Errors` is null or an empty list.
- **Result** — holds the response payload.
- **Errors** — holds the request errors.
