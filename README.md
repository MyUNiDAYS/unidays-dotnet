## Example Usage

```csharp
class Program
{
    static void Main()
    {
        // UNiDAYS will provide your region specific customerId and your signing key
        var customerId = "someCustomerId";
        var signingKey = Encoding.ASCII.GetBytes("someSigningKey");

        // Create a reference to the UNiDAYS object
        var unidays = new TrackingHelper(customerId, signingKey);

        // Depending on use-case, you will want to base your implementation on one of the following methods.
        var trackingUrl = ServerToServerCodedUrl(unidays);
        // var trackingUrl = ServerToServerCodelessUrl(unidays);
        // var trackingUrl = ClientSideCodedUrl(unidays);
        // var trackingUrl = ClientSideCodelessUrl(unidays);

        // If you're making a server-to-server request, you will need to call the generated URL, here is an example of how you could do this.
        var request = (HttpWebRequest)WebRequest.Create(trackingUrl);
        request.GetResponse();
    }

    static string ServerToServerCodedUrl(TrackingHelper unidays)
    {
        var transactionId = "order123";
        var currency = "GBP";
        var orderTotal = 209.00M;
        var itemsUNiDAYSDiscount = 13.00M;
        var code = "UNiDAYSCode";
        var itemsTax = 34.50M;
        var shippingGross = 5.00M;
        var shippingDiscount = 3.00M;
        var itemsGross = 230.00M;
        var itemsOtherDiscount = 10.00M;
        var UNiDAYSDiscountPercentage = 10.00M;
        var newCustomer = 1;

        return unidays.ServerToServerTrackingUrl(transactionId, String.Empty, currency, orderTotal, itemsUNiDAYSDiscount,
            code, itemsTax, shippingGross, shippingDiscount, itemsGross, itemsOtherDiscount, UNiDAYSDiscountPercentage,
            newCustomer);
    }

    static string ServerToServerCodelessUrl(TrackingHelper unidays)
    {
        var transactionId = "order123";
        var memberId = "someMemberId";
        var currency = "GBP";
        var orderTotal = 209.00M;
        var itemsUNiDAYSDiscount = 13.00M;
        var itemsTax = 34.50M;
        var shippingGross = 5.00M;
        var shippingDiscount = 3.00M;
        var itemsGross = 230.00M;
        var itemsOtherDiscount = 10.00M;
        var UNiDAYSDiscountPercentage = 10.00M;
        var newCustomer = 1;

        return unidays.ServerToServerTrackingUrl(transactionId, memberId, currency, orderTotal, itemsUNiDAYSDiscount,
            String.Empty, itemsTax, shippingGross, shippingDiscount, itemsGross, itemsOtherDiscount, UNiDAYSDiscountPercentage,
            newCustomer);
    }

    static string ClientSideCodedUrl(TrackingHelper unidays)
    {
        var transactionId = "order123";
        var currency = "GBP";
        var orderTotal = 209.00M;
        var itemsUNiDAYSDiscount = 13.00M;
        var code = "UNiDAYSCode";
        var itemsTax = 34.50M;
        var shippingGross = 5.00M;
        var shippingDiscount = 3.00M;
        var itemsGross = 230.00M;
        var itemsOtherDiscount = 10.00M;
        var UNiDAYSDiscountPercentage = 10.00M;
        var newCustomer = 1;

        return unidays.ClientSideTrackingPixelUrl(transactionId, String.Empty, currency, orderTotal, itemsUNiDAYSDiscount,
            code, itemsTax, shippingGross, shippingDiscount, itemsGross, itemsOtherDiscount, UNiDAYSDiscountPercentage,
            newCustomer);
    }

    static string ClientSideCodelessUrl(TrackingHelper unidays)
    {
        var transactionId = "order123";
        var memberId = "someMemberId";
        var currency = "GBP";
        var orderTotal = 209.00M;
        var itemsUNiDAYSDiscount = 13.00M;
        var itemsTax = 34.50M;
        var shippingGross = 5.00M;
        var shippingDiscount = 3.00M;
        var itemsGross = 230.00M;
        var itemsOtherDiscount = 10.00M;
        var UNiDAYSDiscountPercentage = 10.00M;
        var newCustomer = 1;

        return unidays.ClientSideTrackingPixelUrl(transactionId, memberId, currency, orderTotal, itemsUNiDAYSDiscount,
            String.Empty, itemsTax, shippingGross, shippingDiscount, itemsGross, itemsOtherDiscount, UNiDAYSDiscountPercentage,
            newCustomer);
    }

    static void VerifyStudentUrl(StudentHelper studentHelper) 
    {
        // Your key as provided by UNiDAYS
        const string unidaysSigningKey = @"tnFUmqDkq1w9eT65hF9okxL1On+d2BQWUyOFLYE3FTOwHjmnt5Sh/sxMA3/i0od3pV5EBfSAmXo//fjIdAE3cIAatX7ZZqVi0Dr8qEYGtku+ZRVbPSmTcEUTA/gXYo3KyL2JqXaZ/qhUvCMbLWyV07qRiFOjyLdOWhioHlJM5io=";
        // Turn key into a byte array
        var key = Convert.FromBase64String(unidaysSigningKey);

        // Obtain parameters from the query string. Be sure to URL Decode them
        var ud_s = "Do/faqh330SGgCnn4t3X4g==";
        var ud_t = "1395741712";
        var ud_h = "i38dJdX+XLKuE4F5tv+Knpl5NPtu5zrdsjnqBQliJEJE4NkMmfurVnUaT46WluRYoD1/f5spAqU36YgeTMCNeg==";

        var studentApiHelper = new UNiDAYS.StudentApiHelper(key);
        var verified = studentApiHelper.VerifyHash(ud_s, ud_t, ud_h);
    }
}
```