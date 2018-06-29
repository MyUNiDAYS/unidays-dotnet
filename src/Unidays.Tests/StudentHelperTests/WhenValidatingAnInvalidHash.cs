/*
The MIT License (MIT)

Copyright (c) 2017 MyUNiDAYS Ltd.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

*/

using FluentAssertions;
using Xunit;

namespace Unidays.Tests.StudentHelperTests
{
    public partial class GivenAStudentHelper
    {
        public class WhenValidatingAnInvalidHash : IClassFixture<StudentHelperFixture>
        {
            private readonly bool verified;

            public WhenValidatingAnInvalidHash(StudentHelperFixture fixture)
            {
                verified = fixture.StudentHelper.VerifyHash("eesNa1l1bUWKHsWfOLemXQ==", "1420070500", "qaOotWTdl1GjooDmgagETc4ov8FPo4U7rE5RDp0Gfnmo4UVe5JDQhQYDgi1CXNwYa8xSXE4B0QmM96kqf4DLsw==");
            }

            [Fact]
            public void ThenTheHashIsNotValid()
            {
                verified.Should().BeFalse();
            }
        }
    }
}