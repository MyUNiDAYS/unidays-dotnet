using System;
using System.Web;
using FluentAssertions;
using Xunit;

namespace Unidays.Tests.UriBuilderTests
{
	public class WhenGeneratingASignedPixelUrlWithOnlyMandatoryParameters
	{
	    private Uri _url;

	    public WhenGeneratingASignedPixelUrlWithOnlyMandatoryParameters()
	    {
		    var directTrackingDetails = new DirectTrackingDetailsBuilder("id of customer", "GBP", "the transaction id")
		                                .Build();

			_url = new UriGenerator().GeneratePixelUrl(new byte[4], directTrackingDetails);
	    }

		[Fact]
		public void TheQueryStringShouldOnlyHaveMandatoryParametersInQueryString()
		{
			HttpUtility.ParseQueryString(_url.Query).Count.Should().Be(4);
		}

		[Theory]
	    [InlineData("CustomerId", "id of customer")]
	    [InlineData("TransactionId", "the transaction id")]
	    [InlineData("Currency", "GBP")]
		[InlineData("Signature", "ADQfOqdx5Wg3Tmf5FYlhOC2+rl+1QjTHma2z+jXNdjcVHcLd7CoSueiFvz4n3Pkvg/o5vhmYVqBsAZv6KvSNeA==")]
		public void TheParameterShouldBeCorrect(string parameter, string result)
	    {
		    var parameters = HttpUtility.ParseQueryString(_url.Query);
		    parameters[parameter].Should().Be(result);
	    }
	}
}
