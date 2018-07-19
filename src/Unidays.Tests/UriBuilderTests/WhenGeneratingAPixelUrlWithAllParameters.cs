﻿using System;
using System.Web;
using FluentAssertions;
using Xunit;

namespace Unidays.Tests.UriBuilderTests
{
	public class WhenGeneratingAPixelUrlWithAllParameters
	{
	    private Uri _url;

	    public WhenGeneratingAPixelUrlWithAllParameters()
	    {
		    var directTrackingDetails = new DirectTrackingDetailsBuilder("id of customer", "GBP", "the transaction id")
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

			_url = new UriGenerator().GeneratePixelUrl(directTrackingDetails);
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
	    public void TheParameterShouldBeCorrect(string parameter, string result)
	    {
		    var parameters = HttpUtility.ParseQueryString(_url.Query);
		    parameters[parameter].Should().Be(result);
	    }
	}
}
