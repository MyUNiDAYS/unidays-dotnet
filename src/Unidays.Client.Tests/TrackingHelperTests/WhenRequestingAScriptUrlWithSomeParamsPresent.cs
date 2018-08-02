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
            public void TheHostShouldBeTrackingMyunidaysCom()
            {
                this.url.Host.Should().Be("tracking.myunidays.com");
            }

            [Fact]
            public void ThePathShouldBeV1_2RedemptionJs()
            {
                this.url.PathAndQuery.Should().StartWith("/v1.2/redemption/js");
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
