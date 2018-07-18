using System;
using System.Linq;
using System.Web;
using FluentAssertions;
using Xunit;

namespace Unidays.Tests.UriBuilderTests
{
	public class WhenGeneratingAPixelUrlWithOnlyMandatoryParameters
	{
	    private Uri _url;

	    public WhenGeneratingAPixelUrlWithOnlyMandatoryParameters()
	    {
		    var directTrackingDetails = new DirectTrackingDetailsBuilder("id of customer", "GBP", "the transaction id")
		                                .Build();

			_url = new UriGenerator().GeneratePixelUrl(directTrackingDetails);
	    }

		[Fact]
		public void TheQueryStringShouldOnlyHaveMandatoryParametersInQueryString()
		{
			HttpUtility.ParseQueryString(_url.Query).Count.Should().Be(3);
		}

		[Theory]
	    [InlineData("CustomerId", "id of customer")]
	    [InlineData("TransactionId", "the transaction id")]
	    [InlineData("Currency", "GBP")]
	    public void TheParameterShouldBeCorrect(string parameter, string result)
	    {
		    var parameters = HttpUtility.ParseQueryString(_url.Query);
		    parameters[parameter].Should().Be(result);
	    }
	}
}
