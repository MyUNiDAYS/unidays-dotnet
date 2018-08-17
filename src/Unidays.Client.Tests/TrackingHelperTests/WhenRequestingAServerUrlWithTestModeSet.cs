using System;
using Xunit;
using FluentAssertions;
using System.Web;

namespace Unidays.Client.Tests.TrackingHelperTests
{
    public partial class GivenATrackingHelper
    {
        public class WhenRequestingAServerUrlWithTestModeSet
        {
            private readonly Uri url;

            public WhenRequestingAServerUrlWithTestModeSet()
            {
                var key = "xCaiGms6eEcRYKqY7hXYPBLizZwY9Z2g/OqyOXa0r7lqZ8Npf78eK+rbnoplH7xCAab/0+h1zLYxfJm62GbgSHfnvjUGEOuh/MtHNALCoXD6Y3YWIrJnlEfym2kmWl7ZQoFyYbZXBTZq0SyCXJAI53ShKIcTPDBM3sNLm70IWns=";
                var directTrackingDetails = new DirectTrackingDetailsBuilder("a partner Id", "GBP", "the transaction id")
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

                url = new TrackingHelper(directTrackingDetails).TrackingServerTestUrl(key);
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
            public void ThePathShouldBePerksRedemptionV1()
            {
                this.url.PathAndQuery.Should().StartWith("/tracking/v1.2/redemption");
            }

            [Theory]
            [InlineData("PartnerId", "a partner Id")]
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
            [InlineData("Test", "True")]
            [InlineData("Signature", "VsP++N2PQ7Jy/hH6wjkVcGRLRkqpyBFyZPCLW7u0UYuXiYvBlggi4SgCQ1GPs5mg3JswBYms8qTwRehFpWhhAg==")]
            public void TheParameterShouldBeCorrect(string parameter, string result)
            {
                var parameters = HttpUtility.ParseQueryString(this.url.Query);
                parameters[parameter].Should().Be(result);
            }
        }
    }
}
