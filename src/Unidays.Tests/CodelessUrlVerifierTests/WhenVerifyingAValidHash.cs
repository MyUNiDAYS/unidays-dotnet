using System;
using FluentAssertions;
using Xunit;

namespace Unidays.Tests.CodelessUrlVerifierTests
{
    public class WhenVerifyingAValidHash
    {
        private readonly CodelessUrlVerifier _codelessUrlVerifier;

        public WhenVerifyingAValidHash()
        {
            var key = "tnFUmqDkq1w9eT65hF9okxL1On+d2BQWUyOFLYE3FTOwHjmnt5Sh/sxMA3/i0od3pV5EBfSAmXo//fjIdAE3cIAatX7ZZqVi0Dr8qEYGtku+ZRVbPSmTcEUTA/gXYo3KyL2JqXaZ/qhUvCMbLWyV07qRiFOjyLdOWhioHlJM5io=";
            _codelessUrlVerifier = new CodelessUrlVerifier(key);
        }

        [Fact]
        public void WhenVerifyingUrlParametersThenTheDateIsCorrect()
        {
            const string ud_s = "eesNa1l1bUWKHsWfOLemXQ==";
            const string ud_t = "1420070400";
            const string ud_h = "qaOotWTdl1GjooDmgagETc4ov8FPo4U7rE5RDp0Gfnmo4UVe5JDQhQYDgi1CXNwYa8xSXE4B0QmM96kqf4DLsw==";

            var verified = _codelessUrlVerifier.VerifyUrlParams(ud_s, ud_t, ud_h);

            verified.Should().Be(new DateTime(Convert.ToInt64(ud_t), DateTimeKind.Utc));
        }

        [Fact]
        public void WhenVerifyingUrlThenTheDateIsCorrect()
        {
            var uri = new Uri("https://test.com?ud_s=eesNa1l1bUWKHsWfOLemXQ%3D%3D&ud_t=1420070400&ud_h=qaOotWTdl1GjooDmgagETc4ov8FPo4U7rE5RDp0Gfnmo4UVe5JDQhQYDgi1CXNwYa8xSXE4B0QmM96kqf4DLsw%3D%3D");
            var verified = _codelessUrlVerifier.VerifyUrl(uri);

            verified.Should().Be(new DateTime(Convert.ToInt64(1420070400), DateTimeKind.Utc));
        }
    }
}
