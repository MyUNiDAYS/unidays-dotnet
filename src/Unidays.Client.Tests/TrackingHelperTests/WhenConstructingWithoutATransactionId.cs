using System;
using Xunit;
using FluentAssertions;

namespace Unidays.Client.Tests.TrackingHelperTests
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
                Action ctor = () => new TrackingHelper(new DirectTrackingDetailsBuilder("a partner Id", "GBP", transactionId).Build());

                ctor.Should().Throw<ArgumentException>()
                    .Which.Message.Should().Be($"TransactionId is required{Environment.NewLine}Parameter name: transactionId");
            }
        }
    }
}
