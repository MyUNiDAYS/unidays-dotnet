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

namespace Unidays
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
	using System.Web;

	/// <summary>
	/// UNiDAYS SDK Helper Class
	/// </summary>
	public sealed class StudentHelper
	{
		readonly byte[] key;
		static readonly DateTime epoc = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

		public StudentHelper(byte[] key)
		{
			if (key == null)
				throw new ArgumentNullException("key", "Key cannot be null");

			if (key.Length == 0)
				throw new ArgumentException("Key cannot be empty", "key");

			this.key = key;
		}

		string SignUrlWithStudentId(StringBuilder builder, string url, string studentId, DateTime timestamp)
		{
			var diff = timestamp.ToUniversalTime() - epoc;
			var ud_t = (long)Math.Floor(diff.TotalSeconds);

			builder
				.Append("?ud_s=")
				.Append(HttpUtility.UrlEncode(studentId))
				.Append("&ud_t=")
				.Append(ud_t);

			SignUrl(builder, "ud_h");
			
			if (url.Contains("?"))
				builder.Replace('?', '&', 0, 1);

			builder.Insert(0, url);

			return builder.ToString();
		}

		string Hash(string plaintext)
		{
			using (var hmac = new HMACSHA512(key))
			{
				hmac.Initialize();
				var buffer = Encoding.ASCII.GetBytes(plaintext);
				var signatureBytes = hmac.ComputeHash(buffer);
				return Convert.ToBase64String(signatureBytes);
			}
		}

		void SignUrl(StringBuilder builder, string param)
		{
			var signature = Hash(builder.ToString());

			builder
				.Append('&')
				.Append(param)
				.Append('=')
				.Append(HttpUtility.UrlEncode(signature));
		}

		/// <summary>
		/// Verifies that a hash is correct
		/// </summary>
		/// <param name="studentId"></param>
		/// <param name="timestamp"></param>
		/// <param name="hash"></param>
		/// <returns></returns>
		public bool VerifyHash(string studentId, string timestamp, string hash)
		{
			var ud_t = long.Parse(timestamp);

			var builder = new StringBuilder();

			builder
				.Append("?ud_s=")
				.Append(HttpUtility.UrlEncode(studentId))
				.Append("&ud_t=")
				.Append(ud_t);

			var generatedHash = Hash(builder.ToString());

			return hash.Equals(generatedHash, StringComparison.InvariantCulture);
		}
	}
}