using System;
using FluentAssertions;
using Xunit;

namespace Unidays.Tests.StudentHelperTests
{
	public class WhenVerifyingAnInvalidHash
	{
		private readonly CodelessUrlVerifier _codelessUrlVerifier;

		public WhenVerifyingAnInvalidHash()
		{
			byte[] key = Convert.FromBase64String("tnFUmqDkq1w9eT65hF9okxL1On+d2BQWUyOFLYE3FTOwHjmnt5Sh/sxMA3/i0od3pV5EBfSAmXo//fjIdAE3cIAatX7ZZqVi0Dr8qEYGtku+ZRVbPSmTcEUTA/gXYo3KyL2JqXaZ/qhUvCMbLWyV07qRiFOjyLdOWhioHlJM5io=");
			_codelessUrlVerifier = new CodelessUrlVerifier(key);
		}

		[Fact]
		public void WhenVerifyingUrlParametersThenNullIsReturned()
		{
			const string ud_s = "eesNa1l1bUWKHsWfOLemXQ==";
			const string ud_t = "1420070500";
			const string ud_h = "qaOotWTdl1GjooDmgagETc4ov8FPo4U7rE5RDp0Gfnmo4UVe5JDQhQYDgi1CXNwYa8xSXE4B0QmM96kqf4DLsw==";

			var verified = _codelessUrlVerifier.VerifyUrlParams(ud_s, ud_t, ud_h);

			verified.Should().Be(null);
		}

		[Fact]
		public void WhenVerifyingUrlThenNullIsReturned()
		{
			var uri = new Uri("https://test.com?ud_s=eesNa1l1bUWKHsWfOLemXQ%3D%3D&ud_t=1420070500&ud_h=qaOotWTdl1GjooDmgagETc4ov8FPo4U7rE5RDp0Gfnmo4UVe5JDQhQYDgi1CXNwYa8xSXE4B0QmM96kqf4DLsw%3D%3D");
			var verified = _codelessUrlVerifier.VerifyUrl(uri);

			verified.Should().Be(null);
		}

		[Fact]
		public void WhenVerifyingAUrlWithStudentMissingAnExceptionIsThrown()
		{
			var uri = new Uri("https://test.com?ud_t=1420070500&ud_h=qaOotWTdl1GjooDmgagETc4ov8FPo4U7rE5RDp0Gfnmo4UVe5JDQhQYDgi1CXNwYa8xSXE4B0QmM96kqf4DLsw%3D%3D");
			Action verified = () => _codelessUrlVerifier.VerifyUrl(uri);

			verified.Should().Throw<ArgumentException>()
					.And.Message.Should().Be("URL does not contain the required query parameters");
		}

		[Fact]
		public void WhenVerifyingAUrlWithTimeMissingAnExceptionIsThrown()
		{
			var uri = new Uri("https://test.com?ud_s=eesNa1l1bUWKHsWfOLemXQ%3D%3D&ud_h=qaOotWTdl1GjooDmgagETc4ov8FPo4U7rE5RDp0Gfnmo4UVe5JDQhQYDgi1CXNwYa8xSXE4B0QmM96kqf4DLsw%3D%3D");
			Action verified = () => _codelessUrlVerifier.VerifyUrl(uri);

			verified.Should().Throw<ArgumentException>()
				.And.Message.Should().Be("URL does not contain the required query parameters");
		}
	}
}
