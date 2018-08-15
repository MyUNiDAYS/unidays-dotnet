using System;
using System.Web;
using FluentAssertions;
using Xunit;

namespace Unidays.Client.Tests.TrackingHelperTests
{
    public partial class GivenATrackingHelper
    {
        public class WhenRequestingAServerUrlWithSomeParamsPresent
        {
            private readonly Uri url;

            public WhenRequestingAServerUrlWithSomeParamsPresent()
            {
                var key = "xCaiGms6eEcRYKqY7hXYPBLizZwY9Z2g/OqyOXa0r7lqZ8Npf78eK+rbnoplH7xCAab/0+h1zLYxfJm62GbgSHfnvjUGEOuh/MtHNALCoXD6Y3YWIrJnlEfym2kmWl7ZQoFyYbZXBTZq0SyCXJAI53ShKIcTPDBM3sNLm70IWns=";

                var directTrackingDetails = new DirectTrackingDetailsBuilder("a partner", "GBP", "the transaction").Build();

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
                this.url.PathAndQuery.Should().StartWith("/v1.2/redemption");
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
            [InlineData("Signature", "capIvWHcE83R/0qZlkPznmhXAbUPP0RnbKXe/6EvbMX9TPEOYDWPf6+gjVAsQR+d7lY3tv9y+qAnO7lEgPDoFg==")]
            public void TheParameterShouldBeCorrect(string parameter, string result)
            {
                var parameters = HttpUtility.ParseQueryString(this.url.Query);
                parameters[parameter].Should().Be(result);
            }
        }
    }
}
