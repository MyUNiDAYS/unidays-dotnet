using System;
using System.Web;
using FluentAssertions;
using Xunit;

namespace Unidays.Client.Tests.TrackingHelperTests
{
    public partial class GivenATrackingHelper
    {
        public class WhenRequestingAScriptUrlWithSomeParamsPresent
        {
            private readonly Uri url;

            public WhenRequestingAScriptUrlWithSomeParamsPresent()
            {
                var directTrackingDetails = new DirectTrackingDetailsBuilder("a partner", "GBP", "the transaction").Build();
                url = new TrackingHelper(directTrackingDetails).TrackingScriptUrl();
            }

             [Fact]
            public void TheSchemeShouldBeHttps()
            {
                this.url.Scheme.Should().Be("https");
            }

            [Fact]
            public void TheHostShouldBeApiMyunidaysCom()
            {
                this.url.Host.Should().Be("api.myunidays.com");
            }

            [Fact]
            public void ThePathShouldBeV1_2RedemptionJs()
            {
                this.url.PathAndQuery.Should().StartWith("/tracking/v1.2/redemption/js");
            }

            [Theory]
            [InlineData("PartnerId", "a partner")]
            [InlineData("TransactionId", "the transaction")]
            [InlineData("Currency", "GBP")]
            [InlineData("OrderTotal", "")]
            [InlineData("ItemsUNiDAYSDiscount", "")]
            [InlineData("Code", "")]
            [InlineData("ItemsTax", "")]
            [InlineData("ShippingGross", "")]
            [InlineData("ShippingDiscount", "")]
            [InlineData("ItemsGross", "")]
            [InlineData("ItemsOtherDiscount", "")]
            [InlineData("UNiDAYSDiscountPercentage", "")]
            [InlineData("NewCustomer", "")]
            public void TheParameterShouldBeCorrect(string parameter, string result)
            {
                var parameters = HttpUtility.ParseQueryString(this.url.Query);
                parameters[parameter].Should().Be(result);
            }
        }
    }
}
