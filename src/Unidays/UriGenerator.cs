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

	    public Uri GeneratePixelUrl(byte[] key, DirectTrackingDetails directTrackingDetails)
	    {
		    var uri = new UriGenerator();

		    var queryString = uri.GenerateQueryString(directTrackingDetails);
		    uri.SignUrl(queryString, key);
			queryString.Insert(0, trackingUrl);
		    queryString.Insert(trackingUrl.Length, ".gif");

		    return new Uri(queryString.ToString());
	    }

		public Uri GenerateServerRequestUrl(byte[] key, DirectTrackingDetails directTrackingDetails)
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
				.Append("&Currency=")
				.Append(HttpUtility.UrlEncode(directTrackingDetails.Currency));

			if (!string.IsNullOrEmpty(directTrackingDetails.MemberId))
				builder.Append("&MemberId=").AppendFormat("{0:0.00}", directTrackingDetails.MemberId);

			if (!string.IsNullOrEmpty(directTrackingDetails.Code))
				builder.Append("&Code=").Append(HttpUtility.UrlEncode(directTrackingDetails.Code));

			if (directTrackingDetails.OrderTotal.HasValue)
				builder.Append("&OrderTotal=").AppendFormat("{0:0.00}", directTrackingDetails.OrderTotal.Value);

			if (directTrackingDetails.ItemsUNiDAYSDiscount.HasValue)
				builder.Append("&ItemsUNiDAYSDiscount=").AppendFormat("{0:0.00}", directTrackingDetails.ItemsUNiDAYSDiscount.Value);
				
			if (directTrackingDetails.ItemsTax.HasValue)
				builder.Append("&ItemsTax=").AppendFormat("{0:0.00}", directTrackingDetails.ItemsTax.Value);

			if (directTrackingDetails.ShippingGross.HasValue)
				builder.Append("&ShippingGross=").AppendFormat("{0:0.00}", directTrackingDetails.ShippingGross.Value);

			if (directTrackingDetails.ShippingDiscount.HasValue)
				builder.Append("&ShippingDiscount=").AppendFormat("{0:0.00}", directTrackingDetails.ShippingDiscount.Value);
			
			if (directTrackingDetails.ItemsGross.HasValue)
				builder.Append("&ItemsGross=").AppendFormat("{0:0.00}", directTrackingDetails.ItemsGross.Value);

			if (directTrackingDetails.ItemsOtherDiscount.HasValue)
				builder.Append("&ItemsOtherDiscount=").AppendFormat("{0:0.00}", directTrackingDetails.ItemsOtherDiscount.Value);

			if (directTrackingDetails.UNiDAYSDiscountPercentage.HasValue)
				builder.Append("&UNiDAYSDiscountPercentage=").AppendFormat("{0:0.00}", directTrackingDetails.UNiDAYSDiscountPercentage.Value);

			if (directTrackingDetails.NewCustomer.HasValue)
				builder.Append("&NewCustomer=").AppendFormat("{0}", directTrackingDetails.NewCustomer);

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
