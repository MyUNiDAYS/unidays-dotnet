<p align="center">
  <img src="/assets/UNiDAYS_Logo.png" />
</p>
<br/>

![Unidays NuGet Badge](https://img.shields.io/nuget/1.2/unidays-dotnet.svg)
[![Build status](https://ci.appveyor.com/api/projects/status/xjfdbra2ea85qd27?svg=true)](https://ci.appveyor.com/project/UNiDAYS/unidays-dotnet)

# UNiDAYS .NET Library

This is the .NET library for integrating with UNiDAYS. This is to be used for coded and codeless integrations. The following documentation provides descriptions of the implementations and examples.

## Contents

[**How to use this code?**](#how-to-use-this-code)

[**Direct Tracking**](#direct-tracking)
- [Parameters](#parameters)
	- [Example Basket](#example-basket)
- [Example Usage](#example-usage)
    - [Get Server URL _(returns url for server to server request)_](#get-server-url)
    - [Get Script URL _(returns url for client to server request)_](#get-script-url)
    - [Tracking Client _(sends server to server request)_](#tracking-client)
    - [Test endpoints](#test-endpoints)
    - [Direct Tracking Details Builder](#direct-tracking-details-builder)

[**Codeless Verification**](#codeless-verification)
- [Codeless API](#codeless-api)
    - [Codeless Url Verifier](#codeless-url-verifier)

[**Contributing**](#contributing)

## How to use this code

- Pull the package from [NuGet]().
- See the example usage section for the type of call you intend to use. Each of these contains an example.

## Direct Tracking

### Parameters

Here is a description of all the available parameters. Which of these you provide to us are dependant on the agreed contract.

### Mandatory Parameters

| Parameter | Description | Data Type | Example |
|---|---|---|---|
| PartnerId | Your PartnerId as provided by UNiDAYS. If you operate in multiple geographic regions you MAY have a different PartnerId for each region | String | XaxptFh0sK8Co6pI== |
| TransactionId | A unique ID for the transaction in your system | String | Order123 |
| Currency | The ISO 4217 currency code | String | GBP |

Having **either** Code or MemberID as a parameter is also mandatory:

| Parameter | Description | Data Type | Example |
|---|---|---|---|
| Code | The UNiDAYS discount code used | String | ABC123 |
| MemberId | Only to be provided if you are using a codeless integration | String | 0LTio6iVNaKj861RM9azJQ== |

### Optional Parameters

Note any of the following properties to which the value is unknown should be omitted from calls. Which of the following values you provide to us will depend on your agreed contract.

| Parameter | Description | Data Type | Example |
|---|---|---|---|
| OrderTotal | Total monetary amount paid, formatted to 2 decimal places | Decimal | 209.00 |
| ItemsUNiDAYSDiscount | Total monetary amount of UNiDAYS discount applied on gross item value `ItemsGross`, formatted to 2 decimal places | Decimal | 13.00 |
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

Below are the three options for implementing your integration. These examples cover both coded and codeless integrations (see the live analytics PDF for details) and include all optional parameters. They are intended as a guideline for implementation.

- [Get Server URL _(returns url for server to server request)_](#get-server-url)
- [Get Script URL _(returns url for client to server request)_](#get-script-url)
- [Tracking Client _(sends server to server request)_](#tracking-client)
- [Test endpoints](#test-endpoints)

### Get Server URL

It is a mandatory requirement that all server URL's are signed. What this means is that you will need to send us the signing key UNiDAYS provide you with as one of the parameters.

#### Making the call

The method to get the URL to make a server-to-server request with is `TrackingServerUrl(key)`. To implement this method you first need to use the `DirectTrackingDetailsBuilder` to create a direct tracking object with the properties you want to send across to us. More details about this builder can be found [here](#direct-tracking-details-builder).

Once the object containing the details you need to send us is created, create a Tracking helper, providing those details as an parameter (`new TrackingHelper(directTrackingDetails)` in the example) and call `.TrackingServerUrl(signingKey)` where signing key is the key provided to you by UNiDAYS

#### Return

A URL will be returned to you, which can then be used to call our API.

#### Example

```csharp
class Program
{
    static void Main()
    {
        // UNiDAYS will provide your partnerId
        var partnerId = "somePartnerId";
        var signingKey = "someSigningKey";

        var directTrackingDetails = new DirectTrackingDetailsBuilder(partnerId, "GBP", "the transaction")
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

        Uri uri = new TrackingHelper(directTrackingDetails).TrackingServerUrl(signingKey);
    }
}
```

### Get Script URL

This is also known as our client to server integration.

#### Unsigned or Signed

It's an option to create a signed url for your Script request. To do this you will need to send us the signing key UNiDAYS provide you with as one of the parameters.

`Uri uri = new TrackingHelper(directTrackingDetails).TrackingScriptUrl(signingKey);`

instead of

`Uri uri = new TrackingHelper(directTrackingDetails).TrackingScriptUrl();`

#### Making the call

The method to get the URL to make a client-to-server request with is `TrackingScriptUrl()` or  `TrackingScriptUrl(key)` if you've chosen to have a signed URL returned. To implement this method you first need to use the `DirectTrackingDetailsBuilder` to create a direct tracking object with the properties you want to send across to us. More details about this builder can be found [here](#direct-tracking-details-builder).

Once the object containing the details you need to send us is created, create a Tracking helper, providing those details as an parameter (`new TrackingHelper(directTrackingDetails)` in the example) and call `.TrackingScriptUrl()` for an unsigned url or `.TrackingScriptUrl(key)` where signing key is the key provided to you by UNiDAYS for a signed URL.

#### Return

A URL will be returned to you which can be placed within a script element on your post-payment/order-success page to call the API.

#### Example

The below example is a request for an unsigned Script URL.

```csharp
class Program
{
    static void Main()
    {
        // UNiDAYS will provide your partnerId
        var partnerId = "somePartnerId";

        var directTrackingDetails = new DirectTrackingDetailsBuilder(partnerId, "GBP", "the transaction")
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

        Uri uri = new TrackingHelper(directTrackingDetails).TrackingScriptUrl();
    }
}
```

### Tracking Client

Calls to the Tracking Client are similar to [get server url](#get-server-url) but rather than returning a URL, UNiDAYS sends the request and returns a response.

It is a mandatory requirement that all Tracking Client calls are provided with a key, as the requests UNiDAYS send are signed.

#### Making the call

To implement this method you first need to use the `DirectTrackingDetailsBuilder` to create a direct tracking object with the properties you want to send across to us. More details about this builder can be found [here](#direct-tracking-details-builder).

Once the object containing the details you need to send us is created, create an instance of the tracking client, providing those details as an parameter, along with the signing key UNiDAYS provided you with (`new TrackingClient(directTrackingDetails, signingKey)` in the example) and call `.SendAsync()`.

#### Return

A HttpResponseMessage is returned

#### Example

The below example sets up some direct tracking details, calls SendAsync on the client, checks if the status code of the response message is a successful call (2xx) then reads out the content as a string.

```csharp
class Program
{
    static async Task Main()
    {
        // UNiDAYS will provide your partnerId and your signing key
        var partnerId = "somePartnerId";
        var signingKey = "someSigningKey";

        var directTrackingDetails = new DirectTrackingDetailsBuilder(partnerId, "GBP", "the transaction id")
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

### Test Endpoints

UNiDAYS provide test endpoints for each of the above types of call which are as follows:

- `TrackingServerTestUrl(string key)`
- `TrackingScriptTestUrl()`
- `TrackingScriptTestUrl(string key)`

These methods add an extra parameter to the URL that is returned to you, or sent for you which looks like this `&Test=True`.

### Direct Tracking Details Builder

The purpose of direct tracking builder is for it to be made intuitive for you to provide the values you need to UNiDAYS as possible.

The parameters on the builder are the mandatory values

`var directTrackingDetails = new DirectTrackingDetailsBuilder(partnerId, currency, transactionId)`

There are then a variety of methods available to build up the information you want to send us which can be chained up per the example. These match up to the [parameters](#parameter) at the top of this document

- WithMemberId(`string`)
- WithCode(`string`)
- WithOrderTotal(`decimal`)
- WithItemsUNiDAYSDiscount(`decimal`)
- WithItemsTax(`decimal`)
- WithShippingGross(`decimal`)
- WithShippingDiscount(`decimal`)
- WithItemsGross(`decimal`)
- WithItemsOtherDiscount(`decimal`)
- WithUNiDAYSDiscountPercentage(`decimal`)
- WithNewCustomer(`bool`)

Only chain the values that you have been asked to provide. You do not need to use all of these methods.

The final call to be chained is `.Build()` which creates the object.

#### Example

```csharp
class Program
{
    static void Main()
    {
        var directTrackingDetails = new DirectTrackingDetailsBuilder("somePartnerId", "GBP", "the transaction")
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
    }
}
```

## Codeless Verification

If you have agreed to provide UNiDAYS Members with a codeless experience, alongside direct tracking, you will also need to implement the 'Codeless API' which will assist you with parsing and validating the signed traffic we direct towards your site.

### Codeless API

### Making the call

First call the CodelessUrlVerifier with the key provided to you by UNiDAYS (`new CodelessUrlVerifier(key)`). Then call the `VerifyUrlParams(ud_s, ud_t, ud_h)` method with the values for ud_s, ud_t and ud_h as the arguments.

| Parameter | Description | Data Type | Max Length | Example |
|---|---|---|---|---|
| ud_s | UNiDAYS verified student ID | String | 256 chars | Do/faqh330SGgCnn4t3X4g== |
| ud_t | Timestamp for the request | String | 64 bits | 1375349460 |
| ud_h | Hash signature of the other two parameters | Base64 String | GBP | o9Cg3q2eVElZxYlJsEAQ== |

#### Return

If the method successfully validates the hash of the incoming request, a DateTime for the request will be returned; else null will be returned.

#### Example

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

## Contributing

This project is set up as an open source project. As such, if there any any suggestions you have for features, for improving the code itself or come across any problems, you can raise them and / or suggest changes in implementation.

If you are interested in contributing to this codebase, please follow the [contributing guidelines](./contributing.md). This contains guides on both contributing directly and raising feature requests or bug reports. Please adhere to our [code of conduct](./CODE_OF_CONDUCT.md) when doing any of the above.
