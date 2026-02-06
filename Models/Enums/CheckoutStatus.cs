using System.ComponentModel;

namespace Maib.Checkout.Api.Connector.Models.Enums;

public enum CheckoutStatus
{
    WaitingForInit,
    Initialized,
    PaymentMethodSelected,
    Completed,
    Expired,
    Abandoned,
    Cancelled,
    Failed
}