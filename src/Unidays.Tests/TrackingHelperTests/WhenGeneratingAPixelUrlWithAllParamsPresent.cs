using System;
using Xunit;
using FluentAssertions;
using System.Web;

namespace Unidays.Tests.TrackingHelperTests
{
    public partial class GivenATrackingHelper
    {
        public class WhenGeneratingAPixelUrlWithAllParamsPresent : IClassFixture<TrackingHelperFixture>
        {
            private readonly Uri url;

            public WhenGeneratingAPixelUrlWithAllParamsPresent(TrackingHelperFixture fixture)
            {
                url = new Uri(fixture.TrackingHelper.ClientSideTrackingPixelUrl("the transaction", "id of student", "GBP", 209.00M, 13.00M, "a code", 34.50M, 5.00M, 3.00M, 230.00M, 10.00M, 10.00M, 1));
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
                this.url.PathAndQuery.Should().StartWith("/perks/redemption/v1.1.gif");
            }

            [Theory]
            [InlineData("CustomerId", "a customer")]
            [InlineData("TransactionId", "the transaction")]
            [InlineData("MemberId", "id of student")]
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
            [InlineData("Signature", "nB3GKjq9wKf+qbQywULuVEunrGEH2nd+qRKjDoT35nsgy0yDoNvYEPZEdD4VkglhxgB8oMYZEKW9CkMFvgV/+A==")]
            public void TheParameterShouldBeCorrect(string parameter, string result)
            {
                var parameters = HttpUtility.ParseQueryString(this.url.Query);
                parameters[parameter].Should().Be(result);
            }
        }
    }
}
