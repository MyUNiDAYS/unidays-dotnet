using System;
using FluentAssertions;
using Xunit;

namespace Unidays.Client.Tests.CodelessUrlVerifierTests
{
    public partial class GivenAStudentHelper
    {
        public class WhenConstructingWithAnInvalidKey
        {
            [Theory]
            [InlineData("", "Key cannot be null or empty")]
            [InlineData(null, "Key cannot be null or empty")]
            [InlineData("Ag=", "Key must be valid Base64")]
            public void ThenAnArgumentExceptionIsThrown(string key, string expectedMessage)
            {
                Action ctor = () => new Unidays.CodelessUrlVerifier(key);

                ctor.Should().Throw<ArgumentException>()
                    .Which.Message.Should().Be($"{expectedMessage}\r\nParameter name: key");
            }
        }
    }
}
