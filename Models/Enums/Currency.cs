using System.ComponentModel;

// ReSharper disable InconsistentNaming

namespace Maib.Checkout.Api.Connector.Models.Enums;

public enum Currency
{
    [Description("MDL")]
    MDL = 498,
    [Description("USD")]
    USD = 840,
    [Description("EUR")]
    EUR = 978,
}