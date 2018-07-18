using System;
using System.Web;
using FluentAssertions;
using Xunit;

namespace Unidays.Tests.TrackingHelperTests
{
    public partial class GivenATrackingHelper
    {
        public class WhenGeneratingAPixelUrlWithSomeParamsPresent 
        {
            private readonly Uri url;

            public WhenGeneratingAPixelUrlWithSomeParamsPresent()
            {
				var directTrackingDetails = new DirectTrackingDetailsBuilder("a customer", "GBP", "the transaction").Build();
	            url = new TrackingHelper(directTrackingDetails).TrackingPixelUrl();
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
                this.url.PathAndQuery.Should().StartWith("/perks/redemption/v1.2.gif");
            }

            [Theory]
            [InlineData("CustomerId", "a customer")]
            [InlineData("TransactionId", "the transaction")]
            [InlineData("Currency", "GBP")]
            public void TheParameterShouldBeCorrect(string parameter, string result)
            {
                var parameters = HttpUtility.ParseQueryString(this.url.Query);
                parameters[parameter].Should().Be(result);
            }
        }
    }
}
