using System;
using Xunit;
using FluentAssertions;

namespace Unidays.Tests.TrackingHelperTests
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
                Action ctor = () => new Unidays.TrackingHelper(new DirectTrackingDetailsBuilder("a customer Id", currency, "the transaction id").Build());

                ctor.Should().Throw<ArgumentException>()
                    .Which.Message.Should().Be("Currency is required\r\nParameter name: currency");
            }
        }
    }
}
