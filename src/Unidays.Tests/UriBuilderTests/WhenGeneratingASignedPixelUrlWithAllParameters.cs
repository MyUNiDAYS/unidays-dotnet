using System;
using System.Web;
using FluentAssertions;
using Xunit;

namespace Unidays.Tests.UriBuilderTests
{
	public class WhenGeneratingASignedPixelUrlWithAllParameters
	{
	    private Uri _url;

	    public WhenGeneratingASignedPixelUrlWithAllParameters()
	    {
		    var directTrackingDetails = new DirectTrackingDetailsBuilder("id of customer", "GBP", "the transaction id")
		                                .SetOrderTotal(209.00m)
		                                .SetItemsUNiDAYSDiscount(13.00m)
		                                .SetCode("a code")
		                                .SetItemsTax(34.50m)
		                                .SetShippingGross(5.00m)
		                                .SetShippingDiscount(3.00m)
		                                .SetItemsGross(230.00m)
		                                .SetItemsOtherDiscount(10.00m)
		                                .SetUNiDAYSDiscountPercentage(10.00m)
		                                .SetNewCustomer(true)
		                                .Build();

			_url = new UriGenerator().GeneratePixelUrl(new byte[4], directTrackingDetails);
	    }

	    [Theory]
	    [InlineData("CustomerId", "id of customer")]
	    [InlineData("TransactionId", "the transaction id")]
	    [InlineData("Currency", "GBP")]
	    [InlineData("OrderTotal", "209.00")]
	    [InlineData("ItemsUNiDAYSDiscount", "13.00")]
	    [InlineData("Code", "a code")]
	    [InlineData("ItemsTax", "34.50")]
	    [InlineData("ShippingGross", "5.00")]
	    [InlineData("ShippingDiscount", "3.00")]
	    [InlineData("ItemsGross", "230.00")]
	    [InlineData("ItemsOtherDiscount", "10.00")]
	    [InlineData("UNiDAYSDiscountPercentage", "10.00")]
	    [InlineData("NewCustomer", "True")]
	    [InlineData("Signature", "oJgMfHKzPrtvlxHwDQndhaCyd6vNYzUfisoMY0PTQcfW+kDweXMPo0Gi3AaOVQpKRMDZkND5TGH/0kGiFpqPDw==")]
	    public void TheParameterShouldBeCorrect(string parameter, string result)
	    {
		    var parameters = HttpUtility.ParseQueryString(_url.Query);
		    parameters[parameter].Should().Be(result);
	    }
	}
}
