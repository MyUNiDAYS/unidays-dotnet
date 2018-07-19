using System;
using Xunit;
using FluentAssertions;
using System.Web;

namespace Unidays.Tests.TrackingHelperTests
{
    public partial class GivenATrackingHelper
    {
        public class WhenRequestingAServerUrlWithAllParamsSet
        {
            private readonly Uri url;

	        public WhenRequestingAServerUrlWithAllParamsSet()
            {
				var key = new byte[] { 0xc4, 0x26, 0xa2, 0x1a, 0x6b, 0x3a, 0x78, 0x47, 0x11, 0x60, 0xaa, 0x98, 0xee, 0x15, 0xd8, 0x3c, 0x12, 0xe2, 0xcd, 0x9c, 0x18, 0xf5, 0x9d, 0xa0, 0xfc, 0xea, 0xb2, 0x39, 0x76, 0xb4, 0xaf, 0xb9, 0x6a, 0x67, 0xc3, 0x69, 0x7f, 0xbf, 0x1e, 0x2b, 0xea, 0xdb, 0x9e, 0x8a, 0x65, 0x1f, 0xbc, 0x42, 0x1, 0xa6, 0xff, 0xd3, 0xe8, 0x75, 0xcc, 0xb6, 0x31, 0x7c, 0x99, 0xba, 0xd8, 0x66, 0xe0, 0x48, 0x77, 0xe7, 0xbe, 0x35, 0x6, 0x10, 0xeb, 0xa1, 0xfc, 0xcb, 0x47, 0x34, 0x2, 0xc2, 0xa1, 0x70, 0xfa, 0x63, 0x76, 0x16, 0x22, 0xb2, 0x67, 0x94, 0x47, 0xf2, 0x9b, 0x69, 0x26, 0x5a, 0x5e, 0xd9, 0x42, 0x81, 0x72, 0x61, 0xb6, 0x57, 0x5, 0x36, 0x6a, 0xd1, 0x2c, 0x82, 0x5c, 0x90, 0x8, 0xe7, 0x74, 0xa1, 0x28, 0x87, 0x13, 0x3c, 0x30, 0x4c, 0xde, 0xc3, 0x4b, 0x9b, 0xbd, 0x8, 0x5a, 0x7b };
				var directTrackingDetails = new DirectTrackingDetailsBuilder("a customer Id", "GBP", "the transaction id")
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

				url = new TrackingHelper(directTrackingDetails).TrackingServerUrl(key);
            }

            [Fact]
            public void TheSchemeShouldBeHttps()
            {
                this.url.Scheme.Should().Be("https");
            }

            [Fact]
            public void TheHostShouldBeTrackingMyunidaysCom()
            {
                this.url.Host.Should().Be("tracking.myunidays.com");
            }

            [Fact]
            public void ThePathShouldBePerksRedemptionV1()
            {
                this.url.PathAndQuery.Should().StartWith("/perks/redemption/v1.2");
            }

            [Theory]
            [InlineData("CustomerId", "a customer Id")]
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
            [InlineData("Signature", "9CwYes3bxSI//VZc+94YVJL9JSht3BH6jtoAAPC/goyyFBEnEuYI31TQCs13LyuegSKC+0yCr63Ql9g3XVeUDw==")]
            public void TheParameterShouldBeCorrect(string parameter, string result)
            {
                var parameters = HttpUtility.ParseQueryString(this.url.Query);
                parameters[parameter].Should().Be(result);
            }
        }
    }
}
