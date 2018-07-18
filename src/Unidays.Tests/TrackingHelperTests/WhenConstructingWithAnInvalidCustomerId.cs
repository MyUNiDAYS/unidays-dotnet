using System;
using Xunit;
using FluentAssertions;

namespace Unidays.Tests.TrackingHelperTests
{
	public partial class GivenATrackingHelper
    {
        public class WhenConstructingWithAnInvalidCustomerId
        {
            [Theory]
            [InlineData("")]
            [InlineData(null)]
            public void ThenAnArgumentExceptionIsThrown(string customerId)
            {
                Action ctor = () => new TrackingHelper(new DirectTrackingDetailsBuilder(customerId, "GBP", "the transaction id").Build());

                ctor.Should().Throw<ArgumentException>()
                    .Which.Message.Should().Be("CustomerId is required\r\nParameter name: customerId");
            }
        }
	}
}
