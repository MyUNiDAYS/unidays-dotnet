using System;
using Xunit;
using FluentAssertions;
using System.Security.Cryptography;

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
                Action ctor = () => new Unidays.TrackingHelper(customerId, null);

                ctor.Should().Throw<ArgumentException>()
                    .Which.Message.Should().Be("CustomerId is required\r\nParameter name: customerId");
            }
        }
    }
}
