using System;
using Xunit;
using FluentAssertions;

namespace Unidays.Client.Tests.TrackingHelperTests
{
    public partial class GivenATrackingHelper
    {
        public class WhenConstructingWithoutACurrency
        {
            [Theory]
            [InlineData("")]
            [InlineData(null)]
            public void ThenAnArgumentExceptionIsThrown(string currency)
            {
                Action ctor = () => new TrackingHelper(new DirectTrackingDetailsBuilder("a partner Id", currency, "the transaction id").Build());

                ctor.Should().Throw<ArgumentException>()
                    .Which.Message.Should().Be($"Currency is required{Environment.NewLine}Parameter name: currency");
            }
        }
    }
}
