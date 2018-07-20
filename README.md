# UNiDAYS Dotnet Tracking Helper

![NuGet](https://img.shields.io/nuget/dt/Microsoft.AspNetCore.Mvc.svg) <- change to our project
Include build tool badge.

This is the .NET library for UNiDAYS redemption tracking. This is to be used for coded and codeless integrations. The following documentation provides descriptions of the implementation, examples for server - server and client - server integrations.

## Parameters

Here is a description of all the available parameters. Which of these you provide to us are dependant on the agreed contract.

Mandatory parameters are:

* `CustomerId`
* `TransactionId`
* `Currency`
* `Code` or `MemberId`

| Parameter | Description | Data Type | Example |
|---|---|---|---|
| TransactionId | A unique ID for the transaction in your system | String | Order123 |
| MemberId | Only to be provided if you are using a codeless integration | String | 0LTio6iVNaKj861RM9azJQ== |
| Currency | The ISO 4217 currency code | String | GBP |
| OrderTotal | Total monetary amount paid, formatted to 2 decimal places | Decimal | 209.00 |
| ItemsUNiDAYSDiscount | Total monetary amount of UNiDAYS discount applied on gross item value `ItemsGross`, formatted to 2 decimal places | Decimal | 13.00 |
| Code | The UNiDAYS discount code used | String | ABC123 |
| ItemsTax | Total monetary amount of tax applied to items, formatted to 2 decimal places | Decimal | 34.50
| ShippingGross | Total monetary amount of shipping cost, before any shipping discount or tax applied, formatted to 2 decimal places | Decimal | 5.00 |
| ShippingDiscount | Total monetary amount of shipping discount (UNiDAYS or otherwise) applied to the order, formatted to 2 decimal places | Decimal | 3.00 |
| ItemsGross | Total monetary amount of the items, including tax, before any discounts are applied, formatted to 2 decimal places | Decimal | 230.00 |
| ItemsOtherDiscount | Total monetary amount of all non UNiDAYS discounts applied to `ItemsGross`, formatted to 2 decimal places | Decimal | 10.00 |
| UNiDAYSDiscountPercentage | The UNiDAYS discount applied, as a percentage, formatted to 2 decimal places | Decimal | 10.00 |
| NewCustomer | Is the user a new (vs returning) customer to you? | Boolean | true or false |

### Example Basket

Here is an example basket with the fields relating to UNiDAYS tracking parameters,

| Item | Gross | UNiDAYS Discount | Other Discount | Tax | Net Total | Line Total |
|---|---|---|---|---|---|---|
| Shoes | 100.00 | 0.00 | 0.00 | 16.67 | 83.33 | 100.00 |
| Shirt | 50.00 | 5.00 | 0.00 | 7.50 | 37.50 | 45.00 |
| Jeans | 80.00 | 8.00 | 10.00 | 10.33 | 51.67 | 62.00 |
||||||||
| **Totals** | 230.00 | 13.00 | 10.00 | 34.50 | 172.50 | 207.00 |
||||||||
|||||| Shipping | 5.00 |
|||||| Shipping Discount | 3.00 |
||||||||
|||||| **Order Total** | 209.00 |

## Example Usage

Below are examples of implementing the server to server and client to server integrations. These examples cover both coded and codeless integrations and include all optional parameters. They are intended as a guideline for implementation.

### Server To Server

```csharp
class Program
{
    static async Task Main()
    {
        // UNiDAYS will provide your region specific customerId and your signing key
        var customerId = "someCustomerId";
        var signingKey = "someSigningKey";

        var directTrackingDetails = new DirectTrackingDetailsBuilder(customerId, "GBP", "the transaction id")
                                        .WithOrderTotal(209.00m)
                                        .WithItemsUNiDAYSDiscount(13.00m)
                                        .WithCode("a code")
                                        .WithItemsTax(34.50m)
                                        .WithShippingGross(5.00m)
                                        .WithShippingDiscount(3.00m)
                                        .WithItemsGross(230.00m)
                                        .WithItemsOtherDiscount(10.00m)
                                        .WithUNiDAYSDiscountPercentage(10.00m)
                                        .WithNewCustomer(true)
                                        .Build();

        var response = await new TrackingClient(directTrackingDetails, signingKey).SendAsync();

        if (!response.IsSuccessStatusCode())
        {
            // The response body contains a json description of the errors
            System.Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }
}
```

### Client To Server

```csharp
class Program
{
    static void Main()
    {
        // UNiDAYS will provide your region specific customerId
        var customerId = "someCustomerId";
        var signingKey = "someSigningKey";

        var directTrackingDetails = new DirectTrackingDetailsBuilder(customerId, "GBP", "the transaction")
                                    .WithOrderTotal(209.00m)
                                    .WithItemsUNiDAYSDiscount(13.00m)
                                    .WithCode("a code")
                                    .WithItemsTax(34.50m)
                                    .WithShippingGross(5.00m)
                                    .WithShippingDiscount(3.00m)
                                    .WithItemsGross(230.00m)
                                    .WithItemsOtherDiscount(10.00m)
                                    .WithUNiDAYSDiscountPercentage(10.00m)
                                    .WithNewCustomer(true)
                                    .Build();

        Uri uri = new TrackingHelper(directTrackingDetails).TrackingPixelUrl(signingKey);
    }
}
```

### VerifyStudentUrl

```csharp
class Program
{
    static void Main()
    {
        // Your key as provided by UNiDAYS
        const string unidaysSigningKey = @"tnFUmqDkq1w9eT65hF9okxL1On+d2BQWUyOFLYE3FTOwHjmnt5Sh/sxMA3/i0od3pV5EBfSAmXo//fjIdAE3cIAatX7ZZqVi0Dr8qEYGtku+ZRVbPSmTcEUTA/gXYo3KyL2JqXaZ/qhUvCMbLWyV07qRiFOjyLdOWhioHlJM5io=";

        // Obtain parameters from the query string. Be sure to URL Decode them
        var ud_s = "Do/faqh330SGgCnn4t3X4g==";
        var ud_t = "1395741712";
        var ud_h = "i38dJdX+XLKuE4F5tv+Knpl5NPtu5zrdsjnqBQliJEJE4NkMmfurVnUaT46WluRYoD1/f5spAqU36YgeTMCNeg==";

        var verifier = new Unidays.CodelessUrlVerifier(unidaysSigningKey);
        DateTime? verifiedAt = verifier.VerifyUrlParams(ud_s, ud_t, ud_h);
    }
}
```
