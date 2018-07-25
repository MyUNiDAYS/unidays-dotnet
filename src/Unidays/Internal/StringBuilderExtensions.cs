using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Unidays.Internal
{
    internal static class StringBuilderExtensions
    {
        public static StringBuilder AppendTrackingParameters(this StringBuilder builder, DirectTrackingDetails directTrackingDetails)
        {
            builder
                .Append("?PartnerId=")
                .Append(HttpUtility.UrlEncode(directTrackingDetails.PartnerId))
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

        public static StringBuilder AppendSignature(this StringBuilder builder, string key)
        {
            using (var hmac = new HMACSHA512())
            {
                hmac.Key = Convert.FromBase64String(key);

                hmac.Initialize();
                var buffer = Encoding.ASCII.GetBytes(builder.ToString());
                var signatureBytes = hmac.ComputeHash(buffer);
                builder.Append($"&Signature={HttpUtility.UrlEncode(Convert.ToBase64String(signatureBytes))}");
                return builder;
            }
        }

        public static StringBuilder AppendTestParameter(this StringBuilder builder, bool isTestUri)
        {
            if (isTestUri)
                builder.Append("&Test=True");
            return builder;
        }
    }
}
