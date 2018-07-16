using System;
using Xunit;
using FluentAssertions;

namespace Unidays.Tests.TrackingHelperTests
{
    public partial class GivenATrackingHelper
    {
        public class WhenConstructingWithAnInvalidKey
        {
            [Theory]
            [InlineData(new byte[0], "Key cannot be empty")]
            [InlineData(null, "Key cannot be null")]
            public void ThenAnArgumentExceptionIsThrown(byte[] key, string expectedMessage)
            {
                Action ctor = () => new Unidays.TrackingHelper("a customer", key);

                ctor.Should().Throw<ArgumentException>()
                    .Which.Message.Should().Be($"{expectedMessage}\r\nParameter name: key");
            }
        }
    }
}
