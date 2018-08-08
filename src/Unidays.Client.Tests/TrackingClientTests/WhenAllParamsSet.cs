using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Unidays.Client.Tests.TrackingClientTests
{
    public partial class GivenATrackingClient
    {
        public class WhenAllParamsSet
        {
            private HttpResponseMessage _response;
            private HttpClient _httpClient;

            public WhenAllParamsSet()
            {
                var key = "xCaiGms6eEcRYKqY7hXYPBLizZwY9Z2g/OqyOXa0r7lqZ8Npf78eK+rbnoplH7xCAab/0+h1zLYxfJm62GbgSHfnvjUGEOuh/MtHNALCoXD6Y3YWIrJnlEfym2kmWl7ZQoFyYbZXBTZq0SyCXJAI53ShKIcTPDBM3sNLm70IWns=";
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

                _httpClient =  new HttpClient(new OkResponseHandler());
                _response = new TrackingClient(directTrackingDetails, key, _httpClient).SendAsync().Result;
            }

            [Fact]
            public void TheStatusCodeShouldBeOk()
            {
                _response.StatusCode.Should().Be(HttpStatusCode.OK);
            }

            private void Dispose()
            {
                _httpClient.Dispose();
            }

            class OkResponseHandler : HttpMessageHandler
            {
                protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
                {
                    return Task.FromResult(new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK
                    });
                }
            }
        }
    }
}
