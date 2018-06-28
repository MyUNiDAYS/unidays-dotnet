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
}
```