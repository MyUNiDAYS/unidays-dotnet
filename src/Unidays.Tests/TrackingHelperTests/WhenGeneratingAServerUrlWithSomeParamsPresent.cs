using System;
using System.Web;
using FluentAssertions;
using Xunit;

namespace Unidays.Tests.TrackingHelperTests
{
    public partial class GivenATrackingHelper
    {
        public class WhenGeneratingAServerUrlWithSomeParamsPresent : IClassFixture<TrackingHelperFixture>
        {
            private readonly Uri url;

            public WhenGeneratingAServerUrlWithSomeParamsPresent(TrackingHelperFixture fixture)
            {
                url = new Uri(fixture.TrackingHelper.ServerToServerTrackingUrl("the transaction", null, null, null, null, null, null, null, null, null, null, null, null));
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
                this.url.PathAndQuery.Should().StartWith("/perks/redemption/v1.1");
            }

            [Theory]
            [InlineData("CustomerId", "a customer")]
            [InlineData("TransactionId", "the transaction")]
            [InlineData("MemberId", "")]
            [InlineData("Currency", "")]
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
            [InlineData("Signature", "pYjMT2MKNrp25IW/aaw51Quq7nPgGJMqs/v75fOF5OGYdOIWw0rZQbQBO3+Bg/knrlyYhUrzBEUs0+020Uhh4A==")]
            public void TheParameterShouldBeCorrect(string parameter, string result)
            {
                var parameters = HttpUtility.ParseQueryString(this.url.Query);
                parameters[parameter].Should().Be(result);
            }
        }
    }
}
