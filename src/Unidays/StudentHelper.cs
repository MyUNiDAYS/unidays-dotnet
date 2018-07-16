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