using System;
using System.Text;
using System.Web;
using FluentAssertions;
using Xunit;

namespace Unidays.Tests.UriBuilderTests
{
    public class WhenGeneratingASignedUrl
	{
	    private StringBuilder _url;

	    public WhenGeneratingASignedUrl()
	    {
		    _url = new UriHelper().GenerateSignedUrl(new byte[4], "id of customer", "the transaction id", "id of member", "GBP", 209.00M, 13.00M, "a code", 34.50M, 5.00M, 3.00M, 230.00M, 10.00M, 10.00M, 1);
	    }

	    [Theory]
	    [InlineData("CustomerId", "id of customer")]
	    [InlineData("TransactionId", "the transaction id")]
	    [InlineData("MemberId", "id of member")]
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
	    [InlineData("NewCustomer", "1")]
	    [InlineData("Signature", "lyzJgoAO47NI/uWJMY4FomcQ70yehFCKxol2M242WOeLHeuCr+/3fTDaRCg9oL0ZjVgXy35IIvMs/Yx1F5YO9g==")]
	    public void TheParameterShouldBeCorrect(string parameter, string result)
	    {
		    var exampleUri = new Uri(_url.Insert(0, "http://www.example.com").ToString());
		    var parameters = HttpUtility.ParseQueryString(exampleUri.Query);
		    parameters[parameter].Should().Be(result);
	    }
	}
}
