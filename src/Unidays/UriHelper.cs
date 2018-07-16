using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Unidays
{
	public class UriHelper
    {

	    public StringBuilder GenerateUnsignedUrl(string customerId, string transactionId, string memberId, string currency, decimal? orderTotal, decimal? itemsUNiDAYSDiscount, string code, decimal? itemsTax, decimal? shippingGross, decimal? shippingDiscount, decimal? itemsGross, decimal? itemsOtherDiscount, decimal? UNiDAYSDiscountPercentage, int? newCustomer)
	    {
		    var queryString = new StringBuilder();
		    var uri = new UriHelper();

			uri.GenerateQueryString(queryString, customerId, transactionId, memberId, currency, orderTotal, itemsUNiDAYSDiscount, code, itemsTax,
			    shippingGross, shippingDiscount, itemsGross, itemsOtherDiscount, UNiDAYSDiscountPercentage, newCustomer);

		    return queryString;
		}

	    public StringBuilder GenerateSignedUrl(byte[] key, string customerId, string transactionId, string memberId, string currency, decimal? orderTotal, decimal? itemsUNiDAYSDiscount, string code, decimal? itemsTax, decimal? shippingGross, decimal? shippingDiscount, decimal? itemsGross, decimal? itemsOtherDiscount, decimal? UNiDAYSDiscountPercentage, int? newCustomer)
	    {
		    var queryString = new StringBuilder();
		    var uri = new UriHelper();

		    uri.GenerateQueryString(queryString, customerId, transactionId, memberId, currency, orderTotal, itemsUNiDAYSDiscount, code, itemsTax,
			    shippingGross, shippingDiscount, itemsGross, itemsOtherDiscount, UNiDAYSDiscountPercentage, newCustomer);
		    uri.SignUrl(queryString, key);

		    return queryString;
	    }

		void GenerateQueryString(StringBuilder builder, string customerId, string transactionId, string memberId, string currency, decimal? orderTotal, decimal? itemsUNiDAYSDiscount, string code, decimal? itemsTax, decimal? shippingGross, decimal? shippingDiscount, decimal? itemsGross, decimal? itemsOtherDiscount, decimal? UNiDAYSDiscountPercentage, int? newCustomer)
	    {
		    builder
			    .Append("?CustomerId=")
			    .Append(HttpUtility.UrlEncode(customerId))
			    .Append("&TransactionId=")
			    .Append(HttpUtility.UrlEncode(transactionId))
			    .Append("&MemberId=")
			    .Append(HttpUtility.UrlEncode(memberId))
			    .Append("&Currency=")
			    .Append(HttpUtility.UrlEncode(currency))
			    .Append("&OrderTotal=");

		    if (orderTotal.HasValue)
			    builder.AppendFormat("{0:0.00}", orderTotal.Value);

		    builder.Append("&ItemsUNiDAYSDiscount=");
		    if (itemsUNiDAYSDiscount.HasValue)
			    builder.AppendFormat("{0:0.00}", itemsUNiDAYSDiscount.Value);

		    builder.Append("&Code=").Append(HttpUtility.UrlEncode(code));

		    builder.Append("&ItemsTax=");
		    if (itemsTax.HasValue)
			    builder.AppendFormat("{0:0.00}", itemsTax.Value);

		    builder.Append("&ShippingGross=");
		    if (shippingGross.HasValue)
			    builder.AppendFormat("{0:0.00}", shippingGross.Value);

		    builder.Append("&ShippingDiscount=");
		    if (shippingDiscount.HasValue)
			    builder.AppendFormat("{0:0.00}", shippingDiscount.Value);

		    builder.Append("&ItemsGross=");
		    if (itemsGross.HasValue)
			    builder.AppendFormat("{0:0.00}", itemsGross.Value);

		    builder.Append("&ItemsOtherDiscount=");
		    if (itemsOtherDiscount.HasValue)
			    builder.AppendFormat("{0:0.00}", itemsOtherDiscount.Value);

		    builder.Append("&UNiDAYSDiscountPercentage=");
		    if (UNiDAYSDiscountPercentage.HasValue)
			    builder.AppendFormat("{0:0.00}", UNiDAYSDiscountPercentage.Value);

		    builder.Append("&NewCustomer=");
		    if (newCustomer.HasValue)
			    builder.AppendFormat("{0}", newCustomer);
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
