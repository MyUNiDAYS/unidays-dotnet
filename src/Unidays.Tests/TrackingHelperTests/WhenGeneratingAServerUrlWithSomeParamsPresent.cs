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
	            var directTrackingDetails = new DirectTrackingDetailsBuilder("a customer", "GBP", "the transaction").Build();
                url = fixture.TrackingHelper.SignedDirectTrackingUrl(directTrackingDetails);
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
            [InlineData("CustomerId", "a customer")]
            [InlineData("TransactionId", "the transaction")]
            [InlineData("MemberId", "")]
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
            [InlineData("Signature", "cQ8Gb9LcU18buwKM5PbsvyFT7d/eJeHwdtg/8Z8UgNjFuGVwTwYLeQRS6rBhMavzhUPPUI7wRl0zX6+cK/pn8A==")]
            public void TheParameterShouldBeCorrect(string parameter, string result)
            {
                var parameters = HttpUtility.ParseQueryString(this.url.Query);
                parameters[parameter].Should().Be(result);
            }
        }
    }
}
