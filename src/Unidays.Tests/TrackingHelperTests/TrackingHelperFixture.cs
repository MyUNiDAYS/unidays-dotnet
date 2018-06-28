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


namespace Unidays.Tests.TrackingHelperTests
{
    public class TrackingHelperFixture 
    {
        public TrackingHelperFixture()
        {
            Key = new byte[] { 0xc4, 0x26, 0xa2, 0x1a, 0x6b, 0x3a, 0x78, 0x47, 0x11, 0x60, 0xaa, 0x98, 0xee, 0x15, 0xd8, 0x3c, 0x12, 0xe2, 0xcd, 0x9c, 0x18, 0xf5, 0x9d, 0xa0, 0xfc, 0xea, 0xb2, 0x39, 0x76, 0xb4, 0xaf, 0xb9, 0x6a, 0x67, 0xc3, 0x69, 0x7f, 0xbf, 0x1e, 0x2b, 0xea, 0xdb, 0x9e, 0x8a, 0x65, 0x1f, 0xbc, 0x42, 0x1, 0xa6, 0xff, 0xd3, 0xe8, 0x75, 0xcc, 0xb6, 0x31, 0x7c, 0x99, 0xba, 0xd8, 0x66, 0xe0, 0x48, 0x77, 0xe7, 0xbe, 0x35, 0x6, 0x10, 0xeb, 0xa1, 0xfc, 0xcb, 0x47, 0x34, 0x2, 0xc2, 0xa1, 0x70, 0xfa, 0x63, 0x76, 0x16, 0x22, 0xb2, 0x67, 0x94, 0x47, 0xf2, 0x9b, 0x69, 0x26, 0x5a, 0x5e, 0xd9, 0x42, 0x81, 0x72, 0x61, 0xb6, 0x57, 0x5, 0x36, 0x6a, 0xd1, 0x2c, 0x82, 0x5c, 0x90, 0x8, 0xe7, 0x74, 0xa1, 0x28, 0x87, 0x13, 0x3c, 0x30, 0x4c, 0xde, 0xc3, 0x4b, 0x9b, 0xbd, 0x8, 0x5a, 0x7b};
            TrackingHelper = new Unidays.TrackingHelper("a customer", this.Key);
        }

        public byte[] Key { get; }
        public TrackingHelper TrackingHelper { get; }
    }
}
