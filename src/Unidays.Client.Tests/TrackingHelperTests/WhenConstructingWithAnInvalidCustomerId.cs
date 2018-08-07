using System;
using Xunit;
using FluentAssertions;

namespace Unidays.Client.Tests.TrackingHelperTests
{
    public partial class GivenATrackingHelper
    {
        public class WhenConstructingWithAnInvalidPartnerId
        {
            [Theory]
            [InlineData("")]
            [InlineData(null)]
            public void ThenAnArgumentExceptionIsThrown(string partnerId)
            {
                Action ctor = () => new TrackingHelper(new DirectTrackingDetailsBuilder(partnerId, "GBP", "the transaction id").Build());

                ctor.Should().Throw<ArgumentException>()
                    .Which.Message.Should().Be($"PartnerId is required{Environment.NewLine}Parameter name: partnerId");
            }
        }
    }
}
