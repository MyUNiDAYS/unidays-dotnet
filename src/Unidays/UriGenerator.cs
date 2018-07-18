using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Unidays
{
	public class UriGenerator
    {
	    const string trackingUrl = "https://tracking.myunidays.com/perks/redemption/v1.2";

	    public Uri GeneratePixelUrl(DirectTrackingDetails directTrackingDetails)
	    {
			var uri = new UriGenerator();

		    var queryString = uri.GenerateQueryString(directTrackingDetails);
		    queryString.Insert(0, trackingUrl);
		    queryString.Insert(trackingUrl.Length, ".gif");

			return new Uri(queryString.ToString());
	    }

		public StringBuilder GenerateUnsignedUrl(DirectTrackingDetails directTrackingDetails)
	    {
		    var queryString = new StringBuilder();
		    var uri = new UriGenerator();

			uri.GenerateQueryString(directTrackingDetails);

		    return queryString;
		}

	    public Uri GenerateSignedUrl(byte[] key, DirectTrackingDetails directTrackingDetails)
	    {
		    var uri = new UriGenerator();

		    var queryString = uri.GenerateQueryString(directTrackingDetails);
		    uri.SignUrl(queryString, key);
		    queryString.Insert(0, trackingUrl);

		    return new Uri(queryString.ToString());
	    }

		public StringBuilder GenerateQueryString(DirectTrackingDetails directTrackingDetails)
		{
			var builder = new StringBuilder();
			builder
				.Append("?CustomerId=")
				.Append(HttpUtility.UrlEncode(directTrackingDetails.CustomerId))
				.Append("&TransactionId=")
				.Append(HttpUtility.UrlEncode(directTrackingDetails.TransactionId))
				.Append("&MemberId=")
				.Append(HttpUtility.UrlEncode(directTrackingDetails.MemberId))
				.Append("&Currency=")
				.Append(HttpUtility.UrlEncode(directTrackingDetails.Currency))
				.Append("&OrderTotal=");

			if (directTrackingDetails.OrderTotal.HasValue)
				builder.AppendFormat("{0:0.00}", directTrackingDetails.OrderTotal.Value);

			builder.Append("&ItemsUNiDAYSDiscount=");
			if (directTrackingDetails.ItemsUNiDAYSDiscount.HasValue)
				builder.AppendFormat("{0:0.00}", directTrackingDetails.ItemsUNiDAYSDiscount.Value);

			builder.Append("&Code=").Append(HttpUtility.UrlEncode(directTrackingDetails.Code));

			builder.Append("&ItemsTax=");
			if (directTrackingDetails.ItemsTax.HasValue)
				builder.AppendFormat("{0:0.00}", directTrackingDetails.ItemsTax.Value);

			builder.Append("&ShippingGross=");
			if (directTrackingDetails.ShippingGross.HasValue)
				builder.AppendFormat("{0:0.00}", directTrackingDetails.ShippingGross.Value);

			builder.Append("&ShippingDiscount=");
			if (directTrackingDetails.ShippingDiscount.HasValue)
				builder.AppendFormat("{0:0.00}", directTrackingDetails.ShippingDiscount.Value);

			builder.Append("&ItemsGross=");
			if (directTrackingDetails.ItemsGross.HasValue)
				builder.AppendFormat("{0:0.00}", directTrackingDetails.ItemsGross.Value);

			builder.Append("&ItemsOtherDiscount=");
			if (directTrackingDetails.ItemsOtherDiscount.HasValue)
				builder.AppendFormat("{0:0.00}", directTrackingDetails.ItemsOtherDiscount.Value);

			builder.Append("&UNiDAYSDiscountPercentage=");
			if (directTrackingDetails.UNiDAYSDiscountPercentage.HasValue)
				builder.AppendFormat("{0:0.00}", directTrackingDetails.UNiDAYSDiscountPercentage.Value);

			builder.Append("&NewCustomer=");
			if (directTrackingDetails.NewCustomer.HasValue)
				builder.AppendFormat("{0}", directTrackingDetails.NewCustomer);

			return builder;
		}

		void SignUrl(StringBuilder builder, byte[] key)
		{
			using (var hmac = new HMACSHA512())
			{
				hmac.Key = key;

				hmac.Initialize();
				var buffer = Encoding.ASCII.GetBytes(builder.ToString());
				var signatureBytes = hmac.ComputeHash(buffer);
				var signature = Convert.ToBase64String(signatureBytes);

				builder
					.Append('&')
					.Append("Signature")
					.Append('=')
					.Append(HttpUtility.UrlEncode(signature));
			}
		}
	}
}
