using System;
using System.Web;
using FluentAssertions;
using Xunit;

namespace Unidays.Tests.TrackingHelperTests
{
    public partial class GivenATrackingHelper
    {
        public class WhenRequestingAPixelUrlWithSomeParamsPresent
        {
            private readonly Uri url;

            public WhenRequestingAPixelUrlWithSomeParamsPresent()
            {
                var directTrackingDetails = new DirectTrackingDetailsBuilder("a partner", "GBP", "the transaction").Build();
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
                this.url.PathAndQuery.Should().StartWith("/v1.2/redemption/gif");
            }

            [Theory]
            [InlineData("PartnerId", "a partner")]
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
