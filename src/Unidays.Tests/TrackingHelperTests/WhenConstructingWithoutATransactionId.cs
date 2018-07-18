using System;
using Xunit;
using FluentAssertions;

namespace Unidays.Tests.TrackingHelperTests
{
	public partial class GivenATrackingHelper
    {
		public class WhenConstructingWithoutATransactionId
	    {
		    [Theory]
		    [InlineData("")]
		    [InlineData(null)]
		    public void ThenAnArgumentExceptionIsThrown(string transactionId)
		    {
			    Action ctor = () => new Unidays.TrackingHelper(new DirectTrackingDetailsBuilder("a customer Id", "GBP", transactionId).Build());

			    ctor.Should().Throw<ArgumentException>()
			        .Which.Message.Should().Be("TransactionId is required\r\nParameter name: transactionId");
		    }
	    }
	}
}
